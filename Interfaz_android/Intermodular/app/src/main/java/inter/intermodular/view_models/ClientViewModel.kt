package inter.intermodular.view_models

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.lifecycle.viewmodel.compose.viewModel
import com.orhanobut.logger.Logger
import inter.intermodular.services.ApiServices
import kotlinx.coroutines.launch

class ClientViewModel : ViewModel() {

    var emailExistsResponse : Boolean by mutableStateOf(true)

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








}