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
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

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

        //TODO crear un usuario Admin por defecto
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
fun ResponseRegister(
    loginRegisterViewModel: LoginRegisterViewModel,
    buttonText: MutableState<String>,
    loading1: MutableState<Boolean>,
    loading2: MutableState<Boolean>,
    loading3: MutableState<Boolean>,
    loading4: MutableState<Boolean>,
    loading5: MutableState<Boolean>
) {
    val res = loginRegisterViewModel.emailExistsResponse
    if (res) {
        Text(text = "Email ya en uso")
        Logger.e("Email en uso")
        buttonText.value = "Volver al Login"

    } else {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center,
            modifier = Modifier
                .fillMaxWidth()
                .padding(10.dp)
        ) {
            Text(text = "Email disponible", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            Text(text = "Cliente Creado", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            if(loading1.value) Text(text = "Usuario Admin Creado", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            if(loading2.value) Text(text = "Zona Comedor Creada", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            if(loading3.value) Text(text = "30 Mesas Creadas", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            if(loading4.value) Text(text = "Familia Barril Creada", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            if(loading5.value) Text(text = "Caña y Pinta Creados", modifier = Modifier.padding(10.dp), fontWeight = FontWeight.Bold, color = colorResource(id = R.color.azul_oscuro))
            Logger.i("Email disponible")
            buttonText.value = "Login"
            LaunchedEffect(Unit){
                delay(400)
                loading1.value = true
                delay(400)
                loading2.value = true
                delay(400)
                loading3.value = true
                delay(400)
                loading4.value = true
                delay(400)
                loading5.value = true
            }
        }
    }
}



@Composable
fun ShowAlertDialog(
    isDialogOpen: MutableState<Boolean>,
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
    buttonText: MutableState<String>
) {
    val scope = rememberCoroutineScope()
    val loading1 = remember { mutableStateOf(false)}
    val loading2 = remember { mutableStateOf(false)}
    val loading3 = remember { mutableStateOf(false)}
    val loading4 = remember { mutableStateOf(false)}
    val loading5 = remember { mutableStateOf(false)}
    var oneClick = true

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
                        text = "REGISTRO",
                        color = Color.Black,
                        fontWeight = FontWeight.Bold,
                        fontSize = 25.sp
                    )

                    Spacer(modifier = Modifier.padding(10.dp))
                    ResponseRegister(
                        loginRegisterViewModel = loginRegisterViewModel,
                        buttonText = buttonText,
                        loading1 = loading1,
                        loading2 = loading2,
                        loading3 = loading3,
                        loading4 = loading4,
                        loading5 = loading5
                    )

                    Spacer(modifier = Modifier.padding(15.dp))

                    Button(
                        onClick = {
                            if(!loginRegisterViewModel.emailExistsResponse){
                                scope.launch {
                                    if(oneClick){
                                        loginRegisterViewModel.createDefaults()
                                        oneClick = false
                                    }
                                    delay(1000)
                                    isDialogOpen.value = false
                                    backRegister = false
                                    navController.navigate(ScreenNav.MapScreen.route)

                                }
                            }else{
                                //Si volvemos al register da problemas con el back button
                                isDialogOpen.value = false
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









