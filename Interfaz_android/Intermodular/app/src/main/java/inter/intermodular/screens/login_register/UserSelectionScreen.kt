package inter.intermodular.screens.login_register

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.layout.VerticalAlignmentLine
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.input.VisualTransformation
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.support.allUsers
import inter.intermodular.support.backUser
import inter.intermodular.support.currentUser
import inter.intermodular.support.defaultAdmin
import inter.intermodular.view_models.LoginRegisterViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

@Composable
fun UserSelection(navController: NavController, loginRegisterViewModel: LoginRegisterViewModel){

    val isDialogOpen = remember { mutableStateOf(false) }
    val inputText = remember { mutableStateOf("")}
    val isAceptarEnabled = remember { mutableStateOf(false)}

    //TODO MANEJAR CON CUIDADO EL BACK BUTTON

    isAceptarEnabled.value = inputText.value.isNotEmpty()

    ShowAlertDialogUser(
        isDialogOpen = isDialogOpen,
        loginRegisterViewModel = loginRegisterViewModel,
        navController = navController,
        inputText = inputText,
        isAceptarEnabled = isAceptarEnabled
    )

    if(!isDialogOpen.value && backUser){
        backUser = false;
        navController.navigate(ScreenNav.UserSelectionScreen.route)
    }
    loginRegisterViewModel.getClientUsersList()
    if(!loginRegisterViewModel.allUsersClientResponse.isNullOrEmpty()){
        allUsers = loginRegisterViewModel.allUsersClientResponse
        drawUsers(navController, loginRegisterViewModel, isDialogOpen)
    }
    else{
        //TODO
        //Informar que no hay users
        //crear un administrador por defecto
        //Indicar la User: Admin Passw 1234 e iniciar sesion con el
    }
}

@Composable
fun ShowAlertDialogUser(
    isDialogOpen: MutableState<Boolean>,
    loginRegisterViewModel: LoginRegisterViewModel,
    navController: NavController,
    inputText: MutableState<String>,
    isAceptarEnabled: MutableState<Boolean>
) {
    var passwordVisibility by remember { mutableStateOf(false) }
    val scope = rememberCoroutineScope()
    var correct by remember { mutableStateOf(false)}
    var checked by remember { mutableStateOf(false)}
    val textAnswer by remember { mutableStateOf("")}

    if (currentUser.name == defaultAdmin.name
        && currentUser.rol == defaultAdmin.name
        && currentUser.passw == defaultAdmin.passw){
            inputText.value = defaultAdmin.passw
            passwordVisibility = true
    }

    if(isDialogOpen.value) {
        Dialog(onDismissRequest = { isDialogOpen.value = false }) {
            Surface(
                modifier = Modifier
                    .width(300.dp)
                    .height(250.dp)
                    .padding(5.dp),
                shape = RoundedCornerShape(5.dp),
                color = Color.White
            ) {
                Column(
                    modifier = Modifier.padding(5.dp),
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center
                ) {
                    /*Spacer(modifier = Modifier.padding(5.dp))

                    Text(
                        text = "INTRODUCE PASSWORD",
                        color = Color.Black,
                        fontWeight = FontWeight.Bold,
                        fontSize = 25.sp
                    )*/

                    OutlinedTextField(
                        value = inputText.value,
                        onValueChange = { it.also { inputText.value = it } },
                        label = { Text(text = "Password", style = TextStyle(
                            color = colorResource(id = R.color.gris_claro)),) },
                        placeholder = { Text(text = "Password",style = TextStyle(
                            color = colorResource(id = R.color.gris_claro)),) },
                        singleLine = true,
                        visualTransformation = if (passwordVisibility) VisualTransformation.None else PasswordVisualTransformation(),
                        keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                        trailingIcon = {
                            val image = if (passwordVisibility) Icons.Filled.Visibility else Icons.Filled.VisibilityOff
                            IconButton(onClick = { passwordVisibility = !passwordVisibility }
                            ) { Icon(imageVector  = image, "Password") }
                        },
                        colors = TextFieldDefaults.outlinedTextFieldColors(
                            focusedBorderColor = colorResource(id = R.color.azul_oscuro),
                            unfocusedBorderColor = colorResource(id = R.color.gris_claro),
                            focusedLabelColor = colorResource(id = R.color.azul_oscuro),
                            unfocusedLabelColor = colorResource(id = R.color.gris_claro),
                            cursorColor = colorResource(id = R.color.azul)
                        ),
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(20.dp, 10.dp),
                    )

                    if(checked){
                      Column(
                          modifier = Modifier
                              .fillMaxWidth()
                              .padding(10.dp),
                          horizontalAlignment = Alignment.CenterHorizontally,
                          verticalArrangement = Arrangement.Center,
                      ) {
                          Text(
                              text = if(correct) "Correct!" else "Incorrect!",
                              color = if(correct) colorResource(id = R.color.verde)
                              else colorResource(id = R.color.rojo),
                              fontWeight = FontWeight.Bold,
                              //modifier = Modifier.fillMaxWidth().padding(10.dp)
                          )
                      }
                    }
                    Button(
                        enabled = isAceptarEnabled.value,
                        onClick = {
                            checked = true
                            if(currentUser.passw.equals(inputText.value)){ //TODO CIFRADO PARA USERS?
                                backUser = false
                                correct = true


                                scope.launch {
                                    delay(1000)
                                    navController.navigate(ScreenNav.MapScreen.route)
                                    isDialogOpen.value = false
                                }

                            }else{
                                correct = false
                                //Si volvemos al register da problemas con el back button
                                //navController.navigate(ScreenNav.UserSelectionScreen.route)
                                scope.launch {
                                    delay(1000)
                                    navController.navigate(ScreenNav.UserSelectionScreen.route)
                                    isDialogOpen.value = false
                                }

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
                            text = "ACEPTAR",
                            color = Color.White,
                            fontSize = 12.sp
                        )
                    }
                }
            }
        }
    }
}


@OptIn(ExperimentalFoundationApi::class)
@Composable
private fun drawUsers(
    navController: NavController,
    loginRegisterViewModel: LoginRegisterViewModel,
    isDialogOpen: MutableState<Boolean>
) {
    Card(
        modifier = Modifier.fillMaxSize()
    ) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
          Row(
          ){
              Text(
                  text = "ELIGE UN USUARIO",
                  fontSize = 20.sp,
                  fontWeight = FontWeight.Bold,
                  color = colorResource(id = R.color.azul_oscuro),
                  modifier = Modifier.padding(0.dp, 20.dp)
              )
              IconButton(
                  onClick = { navController.navigate(ScreenNav.LoginScreen.route)},
                  modifier = Modifier.padding( 10.dp, 8.dp, 10.dp, 10.dp)
              ) {
                  Icon(
                      imageVector = Icons.Filled.HighlightOff,
                      contentDescription = "",
                      tint = colorResource(id = R.color.rojo)
                  )
              }
          }

            LazyColumn(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.spacedBy(15.dp),
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(10.dp, 5.dp)

            ) {
                items(allUsers.count()) { i ->
                    buttonUser(
                        contador = i,
                        navController = navController,
                        loginRegisterViewModel = loginRegisterViewModel,
                        isDialogOpen = isDialogOpen
                    )
                }
            }
        }
    }
}


@Composable
fun buttonUser(
    contador: Int,
    navController: NavController,
    loginRegisterViewModel: LoginRegisterViewModel,
    isDialogOpen: MutableState<Boolean>
) {
    Button(
        modifier = Modifier
            .height(60.dp)
            .fillMaxWidth()
            .padding(3.dp),
        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
        elevation = ButtonDefaults.elevation(
            defaultElevation = 15.dp,
            pressedElevation = 1.dp,
            disabledElevation = 0.dp
        ),
        onClick = {
                //Todo show alert to put password
            currentUser = allUsers[contador]
            backUser = false
            //navController.navigate(ScreenNav.ValidateUserScreen.route)
            isDialogOpen.value = true
        }) {
        Text(
            text = allUsers[contador].name,
            color = Color.White,
            fontWeight = FontWeight.Bold
        )
    }
}



