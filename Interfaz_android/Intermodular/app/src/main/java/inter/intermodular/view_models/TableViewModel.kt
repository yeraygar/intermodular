package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import inter.intermodular.models.FamilyModel
import inter.intermodular.models.ProductModel
import inter.intermodular.models.TicketModel
import inter.intermodular.models.TicketPost
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

    /**
     * UPDATE TICKET
     * UPDATE TABLE
     * UPDATE LINEA_TICKET
     * DELETE TICKET
     * DELETE LINEA_TICKET
     */




}