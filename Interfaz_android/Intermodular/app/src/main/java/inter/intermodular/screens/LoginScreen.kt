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

    var text by remember { mutableStateOf("")}
    val scope = rememberCoroutineScope()

    Column(
        verticalArrangement = Arrangement.Center,
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 50.dp)
    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "LOGIN SCREEN", modifier = Modifier.padding(50.dp)  )
        Spacer(modifier = Modifier.height(18.dp))
        TextField(value = text, onValueChange = {
            text = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(58.dp))

        Button(
            onClick = {
                if(text.isNullOrEmpty()){
                    //TODO nombre no valido
                    Logger.e("Text input login is null or empty")
                }else{
                    //clientViewModel.viewModelScope.launch {
                       scope.launch {
                        //clientViewModel.checkEmail(text)
                       // checkEmail(clientViewModel = clientViewModel, text = text, navController = navController, scope = scope)
                        clientViewModel.checkEmail(text)
                        navController.navigate(ScreenNav.ValidateLoginScreen.withArgs(text))

                       }
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "LOGIN")
        }
        Button(
            onClick = {
                navController.navigate(ScreenNav.RegisterScreen.route){
                    //Nav options
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "Register NOW")
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