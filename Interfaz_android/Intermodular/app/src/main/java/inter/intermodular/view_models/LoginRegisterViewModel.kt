package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import inter.intermodular.models.ClientModel
import inter.intermodular.models.ClientPost
import inter.intermodular.models.UserModel
import inter.intermodular.services.ApiServices
import inter.intermodular.support.currentClient
import inter.intermodular.support.getSHA256
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
                }else Logger.e("Error Response Create Client")
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