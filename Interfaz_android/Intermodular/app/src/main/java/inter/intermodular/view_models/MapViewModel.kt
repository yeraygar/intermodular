package inter.intermodular.view_models

import androidx.activity.OnBackPressedCallback
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.orhanobut.logger.Logger
import inter.intermodular.models.TableModel
import kotlinx.coroutines.launch
import inter.intermodular.models.UserModel
import inter.intermodular.models.ZoneModel
import inter.intermodular.services.ApiServices
import inter.intermodular.support.clientZones
import inter.intermodular.support.currentClient
import inter.intermodular.support.currentZoneTables
import java.lang.Exception

class MapViewModel : ViewModel() {

    var allUsersClientResponse : List<UserModel> by mutableStateOf(listOf())
    var usersFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var usersNoFichadosResponse : List<UserModel> by mutableStateOf(listOf())
    var adminsClientResponse : List<UserModel> by mutableStateOf(listOf())

    var clientZonesResponse : List<ZoneModel> by mutableStateOf(listOf())
    var zoneTablesResponse : List<TableModel> by mutableStateOf((listOf()))

    private var errorMessage : String by mutableStateOf("")

    fun getZoneTables(id_zone : String, onSuccessCallback: () -> Unit){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                zoneTablesResponse = apiServices.getZoneTables(id_zone)
                currentZoneTables = zoneTablesResponse
                Logger.i("SUCCESS getZoneTables")
                onSuccessCallback()
                for(table in currentZoneTables) if(table.id_ticket != "Error") Logger.w("Mesas en current Zone:\n $table")
            }catch(e : Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getZoneTables")
            }
        }
    }

    fun getClientZones(id_client : String) {
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                clientZonesResponse = apiServices.getZones(id_client)
                clientZones = clientZonesResponse
                Logger.i("CORRECT getClientZones")
            }catch(e : Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientZones")
            }
        }
    }

/*    fun getClientUsersList(){
        viewModelScope.launch {
            val apiServices = ApiServices.getInstance()
            try{
                allUsersClientResponse = apiServices.getClientUsers(currentClient._id)
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
                if(buscarFichados) usersFichadosResponse = apiServices.getUsersFichados(currentClient._id)
                else usersNoFichadosResponse = apiServices.getUsersNoFichados(currentClient._id)
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
                adminsClientResponse = apiServices.getClientAdmin(currentClient._id)
                Logger.i("CORRECT getClientAdmins ")
            }catch (e: Exception){
                errorMessage = e.message.toString()
                Logger.e("FAILURE getClientAdmins ")
            }
        }
    }*/
}