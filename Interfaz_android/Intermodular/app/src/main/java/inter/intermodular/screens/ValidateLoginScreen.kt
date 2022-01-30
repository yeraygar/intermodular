package inter.intermodular.screens

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.ScreenNav
import inter.intermodular.ui.theme.Purple500
import inter.intermodular.view_models.ClientViewModel

@Composable
fun ValidateLoginScreen(
    email: String?,
    clientViewModel: ClientViewModel,
    navController: NavController,
    password: String?
) {
    val isDialogOpen = remember { mutableStateOf(true) }
    ShowAlertDialog(isDialogOpen = isDialogOpen, email, clientViewModel, navController)


    //Surface(color = MaterialTheme.colors.background){

    /*   Box(
            //modifier = Modifier.fillMaxWidth()
        ){
            Column {
                Text(text = "Email: $email" )
                response(clientViewModel = clientViewModel, navController = navController, email = email)
                Button(
                    onClick = {
                    if(!clientViewModel.emailExistsResponse){
                        navController.navigate(ScreenNav.MainScreen.withArgs(email?:"Error"))
                    }else{
                        navController.navigate(ScreenNav.LoginScreen.route)
                    }
                }) {
                    Text(text= "Aceptar")

                }
            }
        }
    }*/
//}
}

    @Composable
    fun response(clientViewModel: ClientViewModel, navController: NavController, email: String?) {
        var res = clientViewModel.emailExistsResponse
        if (res) {
            Text(text = "Email ya en uso")
            Text(text = email?: "Error de carga")
            Logger.e("Email en uso")

        } else {
            Text(text = "Email disponible")
            Text(text = email?: "Error de carga")
            Logger.i("Email disponible")

        }
    }



@Composable
fun ShowAlertDialog(
    isDialogOpen: MutableState<Boolean>,
    email: String?,
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

                    /*               Image(
                        painter = painterResource(id = R.drawable.ic_baseline_person_pin_24),
                        contentDescription = "Logo",
                        modifier = Modifier
                            .width(120.dp)
                            .height(120.dp)
                    )

*/

                    Spacer(modifier = Modifier.padding(10.dp))
                    response(
                        clientViewModel = clientViewModel,
                        navController = navController,
                        email = email
                    )

                    OutlinedTextField(
                        value = emailVal.value,
                        onValueChange = { emailVal.value = it },
                        label = { Text(text = "Email Address") },
                        placeholder = { Text(text = "Email Address") },
                        singleLine = true,
                        modifier = Modifier.fillMaxWidth(0.8f)
                    )

                    Spacer(modifier = Modifier.padding(10.dp))

                    /*    OutlinedTextField(
                        value = passwordVal.value,
                        onValueChange = { passwordVal.value = it },
                        trailingIcon = {
                            IconButton(
                                onClick = {
                                    passwordVisibility.value = !passwordVisibility.value
                                }
                            ) {
                                //Icon(painter = painterResource(id = R.drawable.ic_baseline_remove_red_eye_24), contentDescription ="",
                                //    tint = if (passwordVisibility.value) Purple500 else Color.Gray
                                //)
                            }
                        },
                        label = { Text(text = "Password") },
                        placeholder = { Text(text = "Password") },
                        singleLine = true,
                        visualTransformation = if (passwordVisibility.value) VisualTransformation.None else
                            PasswordVisualTransformation(),
                        modifier = Modifier.fillMaxWidth(0.8f)
                    )*/

                    Spacer(modifier = Modifier.padding(15.dp))

                    Button(
                        onClick = {
                            isDialogOpen.value = false
                            if(!clientViewModel.emailExistsResponse){
                                navController.navigate(ScreenNav.MainScreen.withArgs(email?:"Error"))
                            }else{
                                navController.navigate(ScreenNav.LoginScreen.route)

                            }
                        },
                        modifier = Modifier
                            .fillMaxWidth(0.5f)
                            .height(60.dp)
                            .padding(10.dp),
                        shape = RoundedCornerShape(5.dp),
                        colors = ButtonDefaults.buttonColors(Purple500)
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








