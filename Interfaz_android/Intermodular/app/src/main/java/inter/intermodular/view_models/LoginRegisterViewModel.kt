package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import inter.intermodular.models.*
import inter.intermodular.services.ApiServices
import inter.intermodular.support.*
import kotlinx.coroutines.async
import kotlinx.coroutines.awaitAll
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import retrofit2.Response

/**
 * Logica asincrona de llamadas a la Api,
 * vinculado a las vistas del paquete login_register
 * Las funciones se ejecutan en el ViewModelScope y
 * llaman a la instancia de Retrofit con sus rutas.
 */
class LoginRegisterViewModel : ViewModel() {

    var allUsersClientResponse : List<UserModel> by mutableStateOf(listOf())
    var adminsClientResponse : List<UserModel> by mutableStateOf(listOf())

    var emailExistsResponse : Boolean by mutableStateOf(true)
    var currentClientResponse : ClientModel by mutableStateOf(
        ClientModel("Error", "Error", "Error", "Error"))

    var currentZoneResponse : ZoneModel by mutableStateOf(
        ZoneModel("Error", "Error", "Error"))

    var currentUserResponse : UserModel by mutableStateOf(
        UserModel("Error", "Error", "Error", "Error", "Error", "Error", false))

    var currentTableResponse : TableModel by mutableStateOf(
        TableModel("Error", "Error", true, false, "Error", 6, 1,1,10,"Error", "Error"))

    private var errorMessage : String by mutableStateOf("")

    fun checkEmail(email : String){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                emailExistsResponse = apiServices.checkEmail(email)
                Logger.i("CORRECT checkEmail $email, $emailExistsResponse")

            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE checkEmail $email, $emailExistsResponse")
            }
        }
    }

    fun validateClient(email: String, passw : String) {
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val passwEncrypt = getSHA256(passw)
                val list : List<ClientModel> =  apiServices.validateClient(email, passwEncrypt)

                if(list.isNotEmpty()){
                    currentClientResponse = list[0]
                    if(currentClientResponse._id != "Error") currentClient = currentClientResponse
                }
                Logger.i("Result:\n$currentClientResponse \n$currentClient")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE validate client")
            }
        }
    }

    fun createClient(client: ClientPost){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<ClientModel> = apiServices.createClient(client)
                if(response.isSuccessful){
                    currentClientResponse = response.body()!!
                    currentClient = currentClientResponse
                    Logger.i("Create client SUCCESSFUL \n $response \n ${response.body()}")
                }else Logger.e("Error Response Create Client $response")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE create client")
            }
        }
    }

    fun getClientUsersList(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                allUsersClientResponse = apiServices.getClientUsers(currentClient._id)
                Logger.i("CORRECT getClientUsersList ")
            }catch (e : java.lang.Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientUsersList ")
            }
        }
    }

    fun createUser(user : UserPost){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<UserModel> = apiServices.createUser(user)
                if(response.isSuccessful){
                    currentUserResponse = response.body()!!
                    currentUser = currentUserResponse
                    Logger.i("Create User SUCCESSFUL \n $response \n ${response.body()}")
                }else Logger.e("Error Response Create User")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE create User")
            }
        }
    }

    fun createZone(zone : ZonePost){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<ZoneModel> = apiServices.createZone(zone)
                if(response.isSuccessful){
                    currentZoneResponse = response.body()!!
                    currentZone = currentZoneResponse
                    Logger.i("Create Zone SUCCESSFUL \n $response \n ${response.body()}")
                }else Logger.e("Error Response Create Zone")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE create Zone")
            }
        }
    }

    fun createTable(table : TablePost){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val response : Response<TableModel> = apiServices.createTable(table)
                if(response.isSuccessful){
                    currentTableResponse = response.body()!!
                    currentTable = currentTableResponse
                    Logger.i("Create Table SUCCESSFUL \n $response \n ${response.body()}")
                }else Logger.e("Error Response Create Table")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE create Table")
            }
        }
    }

    fun createDefaults(){
        viewModelScope.launch {
            defaultAdmin.id_client = currentClient._id
            createUser(defaultAdmin)
            defaultZone.id_client = currentClient._id
            createZone(defaultZone)
            delay(500)
            currentZone = currentZoneResponse
            defaultTable.id_zone = currentZoneResponse._id
            defaultTable.num_row = 0
            for ( i in 0 until 5){
                defaultTable.num_column = 0
                for(j in 0 until 6){
                    defaultTable.name = "${defaultTable.num_row + 1}${defaultTable.num_column + 1}"
                    createTable(defaultTable)
                    defaultTable.num_column++
                    if(defaultTable.num_column == 5) defaultTable.num_row++
                }
            }
        }
    }


    fun getClientAdmins(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                adminsClientResponse = apiServices.getClientAdmin(currentClient._id)
                Logger.i("CORRECT getClientAdmins ")
            }catch (e: java.lang.Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientAdmins ")
            }
        }
    }
}