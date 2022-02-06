package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import kotlinx.coroutines.launch
import inter.intermodular.models.UserModel
import inter.intermodular.services.ApiServices
import inter.intermodular.support.currentClient
import java.lang.Exception

class MapViewModel : ViewModel() {

    var allUsersClientResponse : List<UserModel> by mutableStateOf(listOf())
    var usersFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var usersNoFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var adminsClientResponse : List<UserModel> by mutableStateOf(listOf())

    private var errorMessage : String by mutableStateOf("")

    fun getClientUsersList(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                allUsersClientResponse = apiServices.getClientUsers("Ecosistema1")
                //allUsersClientResponse = apiServices.getClientUsers(currentClient._id)
                Logger.i("CORRECT getClientUsersList ")
            }catch (e : Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientUsersList ")
            }
        }
    }

    fun getUsersFichados(buscarFichados : Boolean){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                if(buscarFichados) usersFichadosResponse = apiServices.getUsersFichados("Ecosistema1")
                //if(buscarFichados) usersFichadosResponse = apiServices.getUsersFichados(currentClient._id)
                else usersNoFichadosResponse = apiServices.getUsersNoFichados("Ecosistema1")
                //else usersNoFichadosResponse = apiServices.getUsersNoFichados(currentClient._id)
                Logger.i("CORRECT getUsersFichados ${buscarFichados}")
            }catch (e : Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getUsersFichados ${buscarFichados}")

            }
        }
    }

    fun getClientAdmins(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                adminsClientResponse = apiServices.getClientAdmin("Ecosistema1")
                //adminsClientResponse = apiServices.getClientAdmin(currentClient._id)
                Logger.i("CORRECT getClientAdmins ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientAdmins ")
            }
        }
    }
}