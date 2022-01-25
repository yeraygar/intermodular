package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch
import inter.intermodular.models.UserModel
import inter.intermodular.services.ApiServices
import java.lang.Exception

class UserViewModel : ViewModel() {

    var userModelListResponse : List<UserModel> by mutableStateOf(listOf())
    private var errorMessage : String by mutableStateOf("")

    fun getClientUsersList(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                val userList = apiServices.getClientUsers()
                userModelListResponse = userList
            }catch (e : Exception){
                errorMessage = e.message.toString()
            }
        }
    }
}