package inter.intermodular.screens

import androidx.compose.foundation.layout.*
import androidx.compose.material.Button
import androidx.compose.material.Text
import androidx.compose.material.TextField
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.ScreenNav
import inter.intermodular.view_models.ClientViewModel
import kotlinx.coroutines.*

@Composable
fun Login(navController: NavController, clientViewModel: ClientViewModel){
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    Column(
        verticalArrangement = Arrangement.Center,
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 50.dp)
    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text("LOGIN SCREEN", modifier = Modifier.align(Alignment.CenterHorizontally))
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "Email: ")
        TextField(value = email, onValueChange = {
            email = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "Password: ")
        TextField(value = password, onValueChange = {
            password = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(50.dp))
        Button(
            onClick = {
                if(email.isNullOrEmpty() || password.isNullOrEmpty()){
                    //TODO nombre no valido
                    Logger.e("Email or Password input Login is null or empty")
                }else{
                    clientViewModel.validateClient(email = email, passw = password)
                    //se rompe en el navigate
                    //navController.navigate(ScreenNav.ValidateLoginScreen.withArgs(email, password))
                    navController.navigate(ScreenNav.ValidateLoginScreen.route)
                }
            },
            modifier = Modifier.fillMaxWidth().height(50.dp)
        ){
            Text(text = "LOGIN")
        }
        Spacer(modifier = Modifier.height(20.dp))
        Button(
            onClick = {
                navController.navigate(ScreenNav.RegisterScreen.route)
            },
            modifier = Modifier.fillMaxWidth().height(50.dp)
        ){
            Text(text = "to Register")
        }
    }
}