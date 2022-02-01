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
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch

@Composable
fun Register(navController: NavController, clientViewModel: ClientViewModel){

    var email by remember { mutableStateOf("")}
    var password1 by remember { mutableStateOf("")}
    var password2 by remember { mutableStateOf("")}

    val scope = rememberCoroutineScope() //CREO QUE INNECESARIO -> DEJAR COMO EL LOGIN

    Column(
        verticalArrangement = Arrangement.Center,
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 50.dp)
    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "REGISTER SCREEN", modifier = Modifier.align(Alignment.CenterHorizontally)  )
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "Email: ")
        TextField(value = email, onValueChange = {
            email = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "Password: ")
        TextField(value = password1, onValueChange = {
            password1 = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "Repeat Password: ")
        TextField(value = password2, onValueChange = {
            password2 = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(58.dp))
        Button(
            onClick = {
                if(email.isNullOrEmpty()){
                    //TODO nombre no valido
                    Logger.e("Email or password Register is null or empty")
                }else{
                    //clientViewModel.viewModelScope.launch {  TODA LA MOVIDA ESTA PARA ARREGLAR, CREAR REGISTER VALIDATION SCRREN
                    scope.launch {
                        //clientViewModel.checkEmail(text)
                        // checkEmail(clientViewModel = clientViewModel, text = text, navController = navController, scope = scope)
                        clientViewModel.checkEmail(email)
                        navController.navigate(ScreenNav.ValidateRegisterScreen.withArgs(email))

                        //TODO VALIDATE REGISTER SCREEN

                    }
                }
            },
            modifier = Modifier.fillMaxWidth().height(50.dp)
        ){
            Text(text = "REGISTER")
        }
        Spacer(modifier = Modifier.height(20.dp))
        Button(
            onClick = {
                navController.navigate(ScreenNav.LoginScreen.route){
                    //Nav options
                }
            },
            modifier = Modifier.fillMaxWidth().height(50.dp)
        ){
            Text(text = "To Login")
        }
    }
}


private suspend fun checkEmail(
    clientViewModel: ClientViewModel,
    text: String,
    navController: NavController,
    scope: CoroutineScope
) {
    //GlobalScope.launch(Dispatchers.IO) {
    scope.launch {

        clientViewModel.checkEmail(text)
        if (clientViewModel.emailExistsResponse) {
            Logger.e("Email already exists: $text, ${clientViewModel.emailExistsResponse}")
        } else {
            navController.navigate(ScreenNav.MainScreen.withArgs(text))
            Logger.i("Unique email: $text, ${clientViewModel.emailExistsResponse}")
        }
    }
}

