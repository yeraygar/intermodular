package inter.intermodular

import androidx.activity.compose.setContent
import androidx.activity.viewModels
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.compose.foundation.layout.Column
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import inter.intermodular.models.UserModel
import inter.intermodular.ui.theme.IntermodularTheme
import inter.intermodular.view_models.UserViewModel
import com.orhanobut.logger.AndroidLogAdapter
import com.orhanobut.logger.FormatStrategy
import com.orhanobut.logger.Logger
import com.orhanobut.logger.PrettyFormatStrategy


class MainActivity : ComponentActivity() {

    private val userViewModel by viewModels<UserViewModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        Logger.addLogAdapter(AndroidLogAdapter())

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
    var lista : List<UserModel> = userViewModel.allUsersClientResponse


    Column(

    ){
        var usuarioMostrar : String = "Error";
        for(u: UserModel in lista){
            usuarioMostrar = u.name
            Text(text = "Hello $usuarioMostrar!")

        }

    }


}

