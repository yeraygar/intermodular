package inter.intermodular.screens

import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember


import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
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
import inter.intermodular.support.currentClient
import inter.intermodular.ui.theme.Purple500
import inter.intermodular.view_models.ClientViewModel

@Composable
fun ValidateLoginScreen(
    clientViewModel: ClientViewModel,
    navController: NavController,
) {
    val isDialogOpen = remember { mutableStateOf(true) }
    ShowAlertDialogLogin(isDialogOpen = isDialogOpen, clientViewModel, navController)
}

@Composable
fun responseLogin(clientViewModel: ClientViewModel) {
        if (clientViewModel.currentClientResponse._id == "Error") {

        Logger.e("Error, la respuesta no se ha cargado bien")

    } else {
       // Text(text = currentClient.name)
        Text(text = currentClient.email)
        Text(text = currentClient.name)
        Logger.i("Respuesta correcta")

    }
}



@Composable
fun ShowAlertDialogLogin(
    isDialogOpen: MutableState<Boolean>,
    clientViewModel: ClientViewModel,
    navController: NavController
) {
    val emailVal = remember { mutableStateOf("") }
    val passwordVal = remember { mutableStateOf("") }

    val passwordVisibility = remember {
        mutableStateOf(false)
    }
    val focusRequester = remember {
        FocusRequester
    }

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
                        text = "Login Popup",
                        color = Color.Black,
                        fontWeight = FontWeight.Bold,
                        fontSize = 25.sp
                    )

                    Spacer(modifier = Modifier.padding(10.dp))
                    responseLogin(clientViewModel)

                    Spacer(modifier = Modifier.padding(10.dp))

                    Button(
                        onClick = {
                            isDialogOpen.value = false
                            var current = clientViewModel.currentClientResponse
                            if(current._id != "Error"){
                                navController.navigate(ScreenNav.MainScreen.withArgs(current.name?:"Error"))
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
                            text = "Close",
                            color = Color.White,
                            fontSize = 12.sp
                        )
                    }
                }
            }
        }
    }
}




/*
    @Composable
    fun CallAlertDialog() {
        val isDialogOpen = remember { mutableStateOf(false)}

        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center,
            modifier = Modifier
                .fillMaxSize()
                .padding(horizontal = 16.dp)
        ) {
             ShowAlertDialog(isDialogOpen)
            //ValidateLoginScreen(email: String?, clientViewModel: ClientViewModel, navController: NavController, isDialogOpen)

            Button(
                onClick = {
                    isDialogOpen.value = true
                },
                modifier = Modifier
                    .padding(10.dp)
                    .height(50.dp),
                shape = RoundedCornerShape(5.dp),
                colors = ButtonDefaults.buttonColors(Purple500)
            ) {
                Text(
                    text = "Show Popup",
                    color = Color.White
                )
            }
        }
    }




 */










