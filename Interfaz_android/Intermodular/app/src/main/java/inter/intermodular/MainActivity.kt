package inter.intermodular

import androidx.activity.compose.setContent
import androidx.activity.viewModels
import android.os.Bundle
import android.util.Log
import androidx.activity.ComponentActivity
import androidx.compose.foundation.layout.Column
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import inter.intermodular.models.UserModel
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.view_models.UserViewModel

class MainActivity : ComponentActivity() {

    private val userViewModel by viewModels<UserViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            IntermodularTheme {

                Surface(color = MaterialTheme.colors.background) {

                    //TODO implementar navigation bar


                    Greeting(userViewModel)
                }
            }
        }
    }
}

@Composable
fun Greeting(userViewModel : UserViewModel) {
    userViewModel.getClientUsersList()
    var lista : List<UserModel> = userViewModel.userModelListResponse

    Column(

    ){
        var usuarioMostrar : String = "Error";
        for(u: UserModel in lista){
            Log.i("****************************************", u.name)
            usuarioMostrar = u.name
            Text(text = "Hello $usuarioMostrar!")

        }

    }


}

