package inter.intermodular.view_models

import androidx.compose.runtime.MutableState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.SuccessResult
import com.orhanobut.logger.Logger
import inter.intermodular.models.*
import inter.intermodular.screens.table_payment.recalculate
import inter.intermodular.services.ApiServices
import inter.intermodular.support.*
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import retrofit2.Callback
import retrofit2.Response
import java.util.*

class TableViewModel : ViewModel() {

    var currentTicketLineResponse : ProductModel by mutableStateOf(
        ProductModel("Error", "Error",0, 0.0f, 0, 0.0f,
            "Error", "Error", "Error", "Error")
    )

    var currentTicketResponse : TicketModel by mutableStateOf(
        TicketModel("Error", 0f, "Error", "Error",
            "Error", "Error", "Error","Error" , 0, Date(), false, "Error")
    )

    var openTicketResponse : List<TicketModel> by mutableStateOf(listOf())
    var familyProductsResponse : List<ProductModel> by mutableStateOf(listOf())
    var clientFamiliesResponse : List<FamilyModel> by mutableStateOf(listOf())
    var ticketLinesResponse : List<ProductModel> by mutableStateOf(listOf())
    var updateOkResponse : Boolean by mutableStateOf(false)


    private var errorMessage : String by mutableStateOf("")


    fun createTicketLine(
        product: ProductModel,
        currentTicketLines: MutableState<List<ProductModel>>,
        onSuccessCallback: () -> Unit
    ){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            product.id_ticket = currentTicket._id
            val ticketLine = ProductPost(
                name = product.name,
                precio = product.precio,
                cantidad = product.cantidad,
                total = product.total,
                id_client = product.id_client,
                id_familia = product.id_familia,
                id_ticket = product.id_ticket,
                )
            try{
                val response : Response<ProductModel> = apiServices.createTicketLine(ticketLine)
                if (response.isSuccessful){
                    currentTicketLineResponse = response.body()!!
                    var res : MutableList<ProductModel> = currentTicketLines.value as MutableList<ProductModel>
                    res.add(currentTicketLineResponse)
                    currentTicketLines.value = res
                    Logger.i("CORRECT createTicketLine $product")
                    onSuccessCallback()
                    
                }else Logger.e("Response not Successful in createTicketLIne")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE createTicketLine $product")
            }
        }
    }

    fun updateTicketLine(
        productLine: ProductModel,
        productLineId : String,
        onSuccessCallback: () -> Unit
    ){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.updateTicketLine(productLineId, productLine)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS updateTicketLine $response ${response.body()}")
                    onSuccessCallback()
                }else Logger.e("FAILURE response updateTicketLine $productLine in $productLineId")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE update LineTicket\n${e.message.toString()}")
            }
        }
    }

    fun deleteTicketLine(productLineId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.deleteTicketLine(productLineId)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS deleteTicketLine $response ${response.body()}")
                }else Logger.e("FAILURE response DeleteTicketLine for $productLineId")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE delete LineTicket\n${e.message.toString()}")
            }
        }
    }

    fun getTicketLines(ticketId : String, onSuccessCallback: () -> Unit){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                //ticketLinesResponse = listOf()
                ticketLinesResponse = apiServices.getTicketLines(ticketId)
                //allFamilies = ticketLinesResponse
                Logger.i("SUCCESS loading getTicketLines for ticketId: $ticketId")
                onSuccessCallback()
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE loading ticket lines for ticketId: $ticketId")
            }
        }
    }

    fun getTicketLineByName(id_ticket : String, name : String, onSuccessCallback: () -> Unit, onFailureCallback: () -> Unit){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                //if(res.isSuccessful){
                    try{
                        var res = apiServices.getTicketLineByName(id_ticket, name)
                       // currentTicketLineResponse = res
                        if(res.isNullOrEmpty()){
                            Logger.e("current ticket response = error: ")
                            onFailureCallback()

                        }else{
                            Logger.i("on success callback getTicketLineByID: ")
                            currentTicketLineResponse = res[0]
                            onSuccessCallback()
                        }

                    }catch (ex: Exception){
                        errorMessage = ex.message.toString()
                        Logger.e("FAILURE loading ticket lines for ticketId and name PRIMER CATCH: ")
                    }
               // }else onFailureCallback()
            }catch (e: Exception){
               // onFailureCallback()
                errorMessage = e.message.toString()
                Logger.e("FAILURE loading ticket lines for ticketId and name SEGUND CATCH ")
            }
        }
    }

    fun recoverTable(
        tableId: String,
        currentTicketLines: MutableState<List<ProductModel>>,
        productClicked: MutableState<Boolean>
    ){
        viewModelScope.launch {
            try{
                hasOpenTicket(tableId = tableId)
                delay(100)
                if(!openTicketResponse.isNullOrEmpty()){
                   // getTicketLines(openTicketResponse[0]._id)
                    delay(100)
                    Logger.i("Open Ticket True")
                    if(!ticketLinesResponse.isNullOrEmpty()){
                        Logger.i("Mesa recuperada con exito")
                        var res : MutableList<ProductModel> = mutableListOf()
                        for( line in ticketLinesResponse){
                            if(line.name == "Error") deleteTicketLine(line._id)
                            else{


                            var dup = false
                            for( r in res){
                                if (r.name == line.name){
                                    dup = true
                                    r.cantidad ++
                                    deleteTicketLine(line._id)
                                }
                            }
                            if (dup){
                                dup = false

                            }
                            else res.add(line)
                           // res.add(line)
                        }
                        ticketLinesResponse = res
                        currentTicketLines.value = res
                            productClicked.value = true
                    }}
                }
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE recuperar Mesa")
            }
        }
    }

    fun createTicket(onSuccessCallback: () -> Unit){
        viewModelScope.launch {
            val newTicket : TicketPost =
                TicketPost(currentUser._id, currentClient._id, currentTable._id, currentTable.name)
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<TicketModel> = apiServices.createTicket(newTicket)
                if (response.isSuccessful){
                    currentTicketResponse = response.body()!!
                    currentTicket = currentTicketResponse
                    currentTable.id_ticket = currentTicketResponse._id
                    currentTable.ocupada = true
                    updateTable(currentTable, currentTable._id)
                    onSuccessCallback()
                    Logger.i("Create ticket successful ${response.body()}")
                }else Logger.e("Error response CreateTicket $response")

            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE create Ticket\n${e.message.toString()}")
            }
        }
    }



    fun updateTicket(ticket: TicketModel, ticketId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.updateTicket(ticketId, ticket)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS updateTicket $response ${response.body()}")
                }else Logger.e("FAILURE response updateTicket ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE update Ticket\n${e.message.toString()}")
            }
        }
    }

    fun deleteTicket(ticketId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.deleteTicket(ticketId)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS deleteTicket $response ${response.body()}")
                }else Logger.e("FAILURE response DeleteTicket for $ticketId")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE delete Ticket\n${e.message.toString()}")
            }
        }
    }

    fun hasOpenTicket(tableId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                openTicketResponse = listOf()
                openTicketResponse = apiServices.hasTicketOpen(tableId)
                if(!openTicketResponse.isNullOrEmpty()){
                    currentTicket = openTicketResponse[0]
                    currentTable.id_ticket = currentTicket._id
                    currentTable.id_user = currentUser._id
                    updateTable(currentTable, currentTable._id)
                }
                Logger.i("CORRECT hasOpenTicket: $openTicketResponse")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE hasOpenTicket or tableID: $tableId")
            }
        }
    }

    fun getFamilyProducts(familyId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                familyProductsResponse = listOf()
                familyProductsResponse = apiServices.getFamilyProducts(familyId)
                Logger.i("SUCCESS loading getFamilyProducts for familyId: $familyId")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE loading family productos for familyId: $familyId")
            }
        }
    }

    fun getClientFamilies(clientId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                clientFamiliesResponse = listOf()
                clientFamiliesResponse = apiServices.getClientFamilies(clientId)
                allFamilies = clientFamiliesResponse
                Logger.i("SUCCESS loading getClientFamilies for clientId: $clientId")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE loading client families for clientId: $clientId")
            }
        }
    }

    fun updateTable(table: TableModel, tableId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.updateTable(tableId, table)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS updateTable ${currentTicket._id} && ${table.id_ticket} \n $response ${response.body()}")
                }else Logger.e("FAILURE response updateTable ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE update Table\n${e.message.toString()}")
            }
        }
    }

    fun resetTableViewModel(){
        currentTicketLineResponse =
            ProductModel("Error", "Error",0, 0.0f, 0, 0.0f,
                "Error", "Error", "Error", "")


        currentTicketResponse
        TicketModel("Error", 0f, "Error", "Error",
            "Error", "Error", "Error","Error" , 0, Date(), false, "Error")



        openTicketResponse = listOf()
        familyProductsResponse = listOf()
        clientFamiliesResponse = listOf()
        ticketLinesResponse = listOf()
        updateOkResponse = false
    }



}