package inter.intermodular.screens.login_register

import androidx.activity.OnBackPressedCallback
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember


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
import inter.intermodular.support.backLogin
import inter.intermodular.support.currentClient
import inter.intermodular.support.loginIntents
import inter.intermodular.view_models.LoginRegisterViewModel

@Composable
fun ValidateLoginScreen(
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
    activityKiller: () -> Unit,
) {
    val isDialogOpen = remember { mutableStateOf(true) }
    val buttonText = remember { mutableStateOf("")}

    ShowAlertDialogLogin(
        isDialogOpen = isDialogOpen,
        loginRegisterViewModel = loginRegisterViewModel,
        navController = navController,
        buttonText =  buttonText,
        activityKiller = activityKiller
    )

    if(!isDialogOpen.value && backLogin){
        backLogin = false;
        navController.navigate(ScreenNav.LoginScreen.route)
    }

}


@Composable
fun ShowAlertDialogLogin(
    isDialogOpen: MutableState<Boolean>,
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
    buttonText: MutableState<String>,
    activityKiller: () -> Unit
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

                    Spacer(modifier = Modifier.padding(15.dp))

                    ResponseLogin(loginRegisterViewModel, buttonText, activityKiller)

                    Spacer(modifier = Modifier.padding(10.dp))

                    Button(
                        onClick = {
                            isDialogOpen.value = false
                            var current = loginRegisterViewModel.currentClientResponse
                            if(current._id != "Error"){
                                backLogin = false
                                //navController.navigate(ScreenNav.MapScreen.route)
                                navController.navigate(ScreenNav.UserSelectionScreen.route)
                            }else{
                                navController.navigate(ScreenNav.LoginScreen.route)
                            }
                        },
                        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
                        modifier = Modifier
                            .fillMaxWidth(0.5f)
                            .height(60.dp)
                            .padding(10.dp),
                        shape = RoundedCornerShape(5.dp),
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

@Composable
fun ResponseLogin(
    loginRegisterViewModel: LoginRegisterViewModel,
    buttonText: MutableState<String>,
    activityKiller: () -> Unit
) {
    if (loginRegisterViewModel.currentClientResponse._id == "Error") {
        loginIntents--
        Text(
            text = "Login Incorrecto",
            color = Color.Black,
            fontWeight = FontWeight.Bold,
            fontSize = 25.sp
        )
        Spacer(modifier = Modifier.padding(5.dp))
        Text(
            text = "Intentos Restantes: ${loginIntents-1}",
            color = Color.Red,
            fontWeight = FontWeight.Bold,
            fontSize = 15.sp
        )
        buttonText.value = "BACK"
        Logger.e("Error, la respuesta no se ha cargado bien / Login Incorrecto")

        if(loginIntents == 0) { activityKiller() }

    } else {
        Text(
            text = "Login Correcto",
            color = Color.Black,
            fontWeight = FontWeight.Bold,
            fontSize = 25.sp
        )
        Spacer(modifier = Modifier.padding(5.dp))
        Text(text = "Bienvenido")
        Spacer(modifier = Modifier.padding(5.dp))
        Text(text = currentClient.name, fontWeight = FontWeight.Bold)
        buttonText.value = "SIGUIENTE"
        Logger.i("Respuesta correcta")

    }
}











