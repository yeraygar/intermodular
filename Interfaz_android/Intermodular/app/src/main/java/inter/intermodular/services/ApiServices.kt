package inter.intermodular.services

import com.orhanobut.logger.Logger
import inter.intermodular.models.ClientModel
import inter.intermodular.models.ClientPost
import inter.intermodular.models.UserModel
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.*

/**
 * Interfaz que contiene las rutas para llamar a la API y la instancia de Retrofit para hacerlo
 * se tiene que llamar desde el ViewModel
 */
interface ApiServices {

    companion object {
        private var apiServices:ApiServices? = null

        fun getInstance(): ApiServices{
            if(apiServices == null){

                /**TODO CADA UNO TIENE QUE PONER SU IP LOCAL DONDE CORRE LA API*/
                                /**cmd -> ipconfig -> IPv4Address*/

                //val address : Array<String> = arrayOf("http://192.168.56.1:8081/api/", "Pablo")
                val address : Array<String> = arrayOf("http://192.168.1.93:8081/api/", "PabloPhone")
                //val address : Array<String> = arrayOf("http://xxxxxxxxxx:8081/api/", "Yeray")
                //val address : Array<String> = arrayOf("http://xxxxxxxxxx:8081/api/", "Maria")

                apiServices = Retrofit.Builder()
                    .baseUrl(address[0])
                    .addConverterFactory(GsonConverterFactory.create())
                    .build()
                    .create(ApiServices::class.java)

                Logger.w("Connected a la API, ${address[0]}; IP de ${address[1]}")
            }
            return apiServices!!
        }
    }


    /*********************RUTAS**********************/

    @Headers("Accept: Application/json")


    /********************USERS**********************/

    @GET("users/client/{id}")
    suspend fun getClientUsers(@Path("id") id : String) : List<UserModel>

    @GET("users/client/{id}/active")
    suspend fun getUsersFichados(@Path("id") id : String) : List<UserModel>

    @GET("users/client/{id}/inactive")
    suspend fun getUsersNoFichados(@Path("id") id : String) : List<UserModel>

    @GET("users/client/{id}/admin")
    suspend fun getClientAdmin(@Path("id") id : String) : List<UserModel>


    /********************CLIENT**********************/

    @GET("client/email/{email}")
    suspend fun checkEmail(@Path("email") email : String) : Boolean

    @GET("client/validate/{email}/{passwEncrypt}")
    suspend fun validateClient (
        @Path("email") email : String,
        @Path("passwEncrypt") passwEncrypt: String
    ) :List<ClientModel>

    @POST("client")
    suspend fun createClient(
        @Body client : ClientPost
    ) : Response<ClientModel>

}