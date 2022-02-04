package inter.intermodular.screens.login_register

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.models.ClientPost
import inter.intermodular.support.*
import inter.intermodular.view_models.LoginRegisterViewModel

@Composable
fun ValidateRegisterScreen(
    name: String?,
    email: String?,
    password : String?,
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
) {
    if(!loginRegisterViewModel.emailExistsResponse && !clientCreated) {
        loginRegisterViewModel.createClient(ClientPost(name!!, email!!, getSHA256(password!!)))
        clientCreated = true
    }
    val isDialogOpen = remember { mutableStateOf(true) }
    val buttonText = remember { mutableStateOf("")}

    ShowAlertDialog(
        isDialogOpen = isDialogOpen,
        loginRegisterViewModel = loginRegisterViewModel,
        navController = navController,
        buttonText = buttonText
    )

    if(!isDialogOpen.value && backRegister){
        backRegister = false;
        navController.navigate(ScreenNav.LoginScreen.route)
    }
}

@Composable
fun ResponseRegister(loginRegisterViewModel: LoginRegisterViewModel, buttonText: MutableState<String>) {
    val res = loginRegisterViewModel.emailExistsResponse
    if (res) {
        Text(text = "Email ya en uso")
        Logger.e("Email en uso")
        buttonText.value = "Back to Login"

    } else {
        Text(text = "Email disponible")
        Spacer(modifier = Modifier.padding(5.dp))
        Text(text = "Client Creado")
        Logger.i("Email disponible")
        buttonText.value = "Login"
    }
}



@Composable
fun ShowAlertDialog(
    isDialogOpen: MutableState<Boolean>,
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
    buttonText: MutableState<String>
) {
    if(isDialogOpen.value) {
        Dialog(onDismissRequest = { isDialogOpen.value = false }) {
            Surface(
                modifier = Modifier
                    .width(300.dp)
                    .height(450.dp)
                    .padding(5.dp),
                shape = RoundedCornerShape(5.dp),
                color = Color.White
            ) {
                Column(
                    modifier = Modifier.padding(5.dp),
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center
                ) {
                    Spacer(modifier = Modifier.padding(5.dp))

                    Text(
                        text = "REGISTER",
                        color = Color.Black,
                        fontWeight = FontWeight.Bold,
                        fontSize = 25.sp
                    )

                    Spacer(modifier = Modifier.padding(10.dp))
                    ResponseRegister(
                        loginRegisterViewModel = loginRegisterViewModel,
                        buttonText = buttonText
                    )

                    Spacer(modifier = Modifier.padding(15.dp))

                    Button(
                        onClick = {
                            isDialogOpen.value = false
                            if(!loginRegisterViewModel.emailExistsResponse){
                                backRegister = false
                                navController.navigate(ScreenNav.MapScreen.route)
                            }else{
                                //Si volvemos al register da problemas con el back button
                                navController.navigate(ScreenNav.LoginScreen.route)
                            }
                        },
                        modifier = Modifier
                            .fillMaxWidth(0.7f)
                            .height(60.dp)
                            .padding(10.dp),
                        shape = RoundedCornerShape(5.dp),
                        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                    ) {
                        Text(
                            text = buttonText.value,
                            color = Color.White,
                            fontSize = 12.sp
                        )
                    }
                }
            }
        }
    }
}









