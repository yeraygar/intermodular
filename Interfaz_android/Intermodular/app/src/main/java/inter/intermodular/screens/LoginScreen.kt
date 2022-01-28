package inter.intermodular.screens

import android.widget.Toast
import androidx.compose.animation.expandHorizontally
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
import inter.intermodular.models.UserModel
import inter.intermodular.view_models.UserViewModel
import kotlinx.coroutines.currentCoroutineContext


@Composable
fun Login(navController: NavController){
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
                    navController.navigate(ScreenNav.MainScreen.withArgs(text)){
                        //Nav options
                    }
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "to Main")
        }
        Button(
            onClick = {
                navController.navigate(ScreenNav.RegisterScreen.route){
                    //Nav options
                }
            },
            modifier = Modifier.align(Alignment.End)
        ){
            Text(text = "to Register")
        }
    }
}