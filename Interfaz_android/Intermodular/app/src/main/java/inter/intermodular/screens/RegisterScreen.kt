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
import inter.intermodular.ScreenNav

@Composable
fun Register(navController: NavController){
    var text by remember {
        mutableStateOf("")
    }
    Column(
        verticalArrangement = Arrangement.Center,
        modifier = Modifier
            .fillMaxWidth()
            .padding(horizontal = 50.dp)
    ){
        Spacer(modifier = Modifier.height(18.dp))
        Text("REGISTER SCREEN")
        Spacer(modifier = Modifier.height(18.dp))
        TextField(value = text, onValueChange = {
            text = it
        },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(8.dp))
        Button(
            onClick = {
                navController.navigate(ScreenNav.MainScreen.withArgs(text)){
                    //Nav options
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "to Main")
        }
        Button(
            onClick = {
                navController.navigate(ScreenNav.LoginScreen.route){
                    //Nav options
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "to Login")
        }
    }
}