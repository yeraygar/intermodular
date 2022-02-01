package inter.intermodular.services

import com.orhanobut.logger.Logger
import inter.intermodular.models.ClientModel
import inter.intermodular.models.UserModel
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Headers
import retrofit2.http.Path

interface ApiServices {

    companion object {
        private var apiServices:ApiServices? = null

        fun getInstance(): ApiServices{
            if(apiServices == null){

                /**TODO CADA UNO TIENE QUE PONER SU IP LOCAL DONDE CORRE LA API*/
                                /**cmd -> ipconfig -> IPv4Adress*/

                val adress : Array<String> = arrayOf("http://192.168.56.1:8081/api/", "Pablo")
                //val adress : Array<String> = arrayOf("http://xxxxxxxxxx:8081/api/", "Yeray")
                //val adress : Array<String> = arrayOf("http://xxxxxxxxxx:8081/api/", "Maria")

                apiServices = Retrofit.Builder()
                    .baseUrl(adress[0])
                    .addConverterFactory(GsonConverterFactory.create())
                    .build()
                    .create(ApiServices::class.java)

                Logger.w("Conectado a la API, ${adress[0]}; IP de ${adress[1]}")
            }
            return apiServices!!
        }
    }


    /*********************RUTAS**********************/

    @Headers("Accept: Application/json")


    /********************USERS**********************/

    @GET("users/client/Ecosistema1")
    suspend fun getClientUsers() : List<UserModel>

    @GET("users/client/Ecosistema1/active")
    suspend fun getUsersFichados() : List<UserModel>

    @GET("users/client/Ecosistema1/inactive")
    suspend fun getUsersNoFichados() : List<UserModel>

    @GET("users/client/Ecosistema1/admin")
    suspend fun getClientAdmin() : List<UserModel>


    /********************CLIENT**********************/

    @GET("client/email/{email}")
    suspend fun checkEmail(@Path("email") email : String) : Boolean

    @GET("client/validate/{email}/{passwEncrypt}")
    suspend fun validateClient (
        @Path("email") email : String,
        @Path("passwEncrypt") passwEncrypt: String
    ) :List<ClientModel>


}