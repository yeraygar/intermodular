package inter.intermodular.screens

import androidx.compose.foundation.layout.*
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import inter.intermodular.models.UserModel
import inter.intermodular.support.currentClient
import inter.intermodular.view_models.ClientViewModel
import inter.intermodular.view_models.UserViewModel

@Composable
fun MainScreen(email: String?, clientViewModel: ClientViewModel){

    Surface(color = MaterialTheme.colors.background) {

        Box(
            modifier = Modifier
                .fillMaxSize()

        ){
            Column() {
                Spacer(modifier = Modifier.height(18.dp))
                Text(text = email ?: "No se ha cargado bien")
                Spacer(modifier = Modifier.height(18.dp))
                Text(text = currentClient.email)
                //AllUsersClient(clientViewModel)
            }
        }
    }
}

/*
@Composable
fun AllUsersClient(clientViewModel: ClientViewModel : UserViewModel) {
    userViewModel.getClientUsersList()
    var lista : List<UserModel> = userViewModel.allUsersClientResponse

    Column(

    ){
        var usuarioMostrar : String = "Error";
        for(u: UserModel in lista){
            usuarioMostrar = u.name
            Text(text = "Hello $usuarioMostrar!")
        }
    }
}*/
