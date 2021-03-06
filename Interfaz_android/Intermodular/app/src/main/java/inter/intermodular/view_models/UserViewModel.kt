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
import java.lang.Exception

class UserViewModel : ViewModel() {

    var allUsersClientResponse : List<UserModel> by mutableStateOf(listOf())
    var usersFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var usersNoFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var adminsClientResponse : List<UserModel> by mutableStateOf(listOf())

    private var errorMessage : String by mutableStateOf("")

    fun getClientUsersList(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                allUsersClientResponse = apiServices.getClientUsers()
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
                if(buscarFichados) usersFichadosResponse = apiServices.getUsersFichados()
                else usersNoFichadosResponse = apiServices.getUsersNoFichados()
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
                adminsClientResponse = apiServices.getClientAdmin()
                Logger.i("CORRECT getClientAdmins ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientAdmins ")
            }
        }
    }
}