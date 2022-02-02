package inter.intermodular.screens.login_register

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Visibility
import androidx.compose.material.icons.filled.VisibilityOff
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.input.VisualTransformation
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.view_models.ClientViewModel

@Composable
fun Register(navController: NavController, clientViewModel: ClientViewModel){

    var email = remember { mutableStateOf("")}
    var password1 = remember { mutableStateOf("")}
    var password2 = remember { mutableStateOf("")}
    var passwordVisibility by remember { mutableStateOf(false) }


    Column(
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally,
        modifier = Modifier
            .fillMaxSize()
            .padding(horizontal = 50.dp)
    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text(text = "REGISTER SCREEN", modifier = Modifier.align(Alignment.CenterHorizontally)  )
        Spacer(modifier = Modifier.height(18.dp))
        OutlinedTextField(
            value = email.value,
            onValueChange = { email.value = it },
            label = { Text(text = "Email Address") },
            placeholder = { Text(text = "Email Address") },
            singleLine = true,
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(18.dp))
        OutlinedTextField(
            value = password1.value,
            onValueChange = { password1.value = it },
            label = { Text(text = "Password", style = TextStyle(
                color = colorResource(id = R.color.azul_oscuro)),) },
            placeholder = { Text(text = "Password",style = TextStyle(
                color = colorResource(id = R.color.gris_claro)),) },
            singleLine = true,
            visualTransformation = if (passwordVisibility) VisualTransformation.None else PasswordVisualTransformation(),
            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Password),
            trailingIcon = {
                val image = if (passwordVisibility) Icons.Filled.Visibility else Icons.Filled.VisibilityOff
                IconButton(onClick = { passwordVisibility = !passwordVisibility }
                ) {
                    Icon(imageVector  = image, "")
                }
            },
            colors = TextFieldDefaults.outlinedTextFieldColors(
                focusedBorderColor = colorResource(id = R.color.azul_oscuro)),

            modifier = Modifier.fillMaxWidth(),

        )

        Spacer(modifier = Modifier.height(18.dp))
        OutlinedTextField(
            value = password2.value,
            onValueChange = { password2.value = it },
            label = { Text(text = "Repeat Password") },
            placeholder = { Text(text = "Repeat Password") },
            singleLine = true,
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(40.dp))
        Button(
            onClick = {
                if(email.value.isNullOrEmpty() || password1.value.isNullOrEmpty() || password2.value.isNullOrEmpty()){
                    //TODO nombre o passwords vacios
                    Logger.e("Email or password Register is null or empty")
                }else{
                    if(password1.value == password2.value){
                        clientViewModel.checkEmail(email.value)
                        navController.navigate(ScreenNav.ValidateRegisterScreen.withArgs(email.value))
                    }
                }
            },
            modifier = Modifier
                .fillMaxWidth()
                .height(50.dp),
            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),

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
            modifier = Modifier
                .fillMaxWidth()
                .height(50.dp),
            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
            ){
            Text(text = "To Login")
        }
    }
}


/*private suspend fun checkEmail(
    clientViewModel: ClientViewModel,
    text: String,
    navController: NavController,
    scope: CoroutineScope
) {

        clientViewModel.checkEmail(text)
        if (clientViewModel.emailExistsResponse) {
            Logger.e("Email already exists: $text, ${clientViewModel.emailExistsResponse}")
        } else {
            navController.navigate(ScreenNav.MainScreen.withArgs(text))
            Logger.i("Unique email: $text, ${clientViewModel.emailExistsResponse}")
        }
    }*/


