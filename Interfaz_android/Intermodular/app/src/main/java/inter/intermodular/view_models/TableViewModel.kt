package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import inter.intermodular.models.*
import inter.intermodular.services.ApiServices
import inter.intermodular.support.currentClient
import inter.intermodular.support.currentTable
import inter.intermodular.support.currentTicket
import inter.intermodular.support.currentUser
import kotlinx.coroutines.launch
import retrofit2.Response
import java.util.*

class TableViewModel : ViewModel() {

    var currentTicketLineResponse : ProductModel by mutableStateOf(
        ProductModel("Error", 0, 0.0f, 0, 0.0f,
            "Error", "Error", "Error", "Error")
    )

    var currentTicketResponse : TicketModel by mutableStateOf(
        TicketModel("Error", 0f, "Error", "Error",
            "Error", "Error", "Error", 2, Date(),false)
    )

    var openTicketResponse : List<TicketModel> by mutableStateOf(listOf())
    var familyProductsResponse : List<ProductModel> by mutableStateOf(listOf())
    var clientFamiliesResponse : List<FamilyModel> by mutableStateOf(listOf())
    var updateOkResponse : Boolean by mutableStateOf(false)


    private var errorMessage : String by mutableStateOf("")


    fun createTicketLine(product : ProductModel){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<ProductModel> = apiServices.createTicketLine(product)
                if (response.isSuccessful){
                    currentTicketLineResponse = response.body()!!
                    Logger.i("CORRECT createTicketLine $product")
                }else Logger.e("Response not Successful in createTicketLIne")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE createTicketLine $product")
            }
        }
    }

    fun updateTicketLine(productLine: ProductModel, productLineId : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            updateOkResponse = false
            try{
                val response = apiServices.updateTicketLine(productLineId, productLine)
                if (response.isSuccessful){
                    updateOkResponse = true
                    Logger.i("SUCCESS updateTicketLine $response ${response.body()}")
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

    fun createTicket(){
        viewModelScope.launch {
            val newTicket : TicketPost =
                TicketPost(currentUser._id, currentClient._id, currentTable._id, currentTable.name)
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<TicketModel> = apiServices.createTicket(newTicket)
                if (response.isSuccessful){
                    currentTicketResponse = response.body()!!
                    currentTicket = currentTicketResponse
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
                    currentTable.id_ticket
                    currentTable.id_user = currentUser._id
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
                    Logger.i("SUCCESS updateTable $response ${response.body()}")
                }else Logger.e("FAILURE response updateTable ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE update Table\n${e.message.toString()}")
            }
        }
    }

}