package inter.intermodular.screens.login_register

import androidx.compose.animation.expandHorizontally
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.view_models.ClientViewModel

@Composable
fun Login(navController: NavController, clientViewModel: ClientViewModel){

    var email = remember { mutableStateOf("") }
    var password = remember { mutableStateOf("") }
    Column(
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally,
        modifier = Modifier
            .fillMaxSize()
            .padding(horizontal = 50.dp)

    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text(
            text ="LOGIN SCREEN",
            fontSize = 20.sp,
            fontWeight = FontWeight.ExtraBold,
            modifier = Modifier.align(Alignment.CenterHorizontally)
        )
        Spacer(modifier = Modifier.height(30.dp))
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
            value = password.value,
            onValueChange = { password.value = it },
            label = { Text(text = "Password") },
            placeholder = { Text(text = "Password") },
            singleLine = true,
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(40.dp))
        Button(
            onClick = {
                if(email.value.isNullOrEmpty() || password.value.isNullOrEmpty()){
                    Logger.e("Email or Password input Login is null or empty")
                }else{
                    clientViewModel.validateClient(email = email.value, passw = password.value)
                    navController.navigate(ScreenNav.ValidateLoginScreen.route)
                }
            },
            modifier = Modifier
                .fillMaxWidth()
                .height(50.dp),
            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),

            ){
            Text(
                text = "LOGIN",
                fontWeight = FontWeight.ExtraBold,
                color = Color.White
            )
        }
        Spacer(modifier = Modifier.height(20.dp))
        Button(
            onClick = {
                navController.navigate(ScreenNav.RegisterScreen.route)
            },
            modifier = Modifier
                .fillMaxWidth()
                .height(50.dp),
            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
            ){
            Text(
                text = "to Register",
                fontWeight = FontWeight.ExtraBold,
                color = Color.White
            )
        }
    }
}