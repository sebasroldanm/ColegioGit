using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Datos;
using Utilitarios;
using System.Data;




namespace Logica
{
    public  class LUser : System.Web.UI.Page
    {
        public UUser loggear(string userName, string clave)
        {
            UUser user = new UUser();
            DUser datos = new DUser();

            user.UserName = userName;
            user.Clave = clave;

            user.Mensaje = "";

            DataTable resultado = datos.loggin(user);

            if (resultado.Rows.Count > 0)
            {
                user.SUserId = resultado.Rows[0]["id_usua"].ToString();
                user.SUserName = resultado.Rows[0]["user_name"].ToString();
                user.SNombre = resultado.Rows[0]["nombre_usua"].ToString();
                user.SApellido = resultado.Rows[0]["apellido_usua"].ToString();
                user.SClave = resultado.Rows[0]["clave"].ToString();
                user.SCorreo = resultado.Rows[0]["correo"].ToString();
                user.SDocumento = resultado.Rows[0]["num_documento"].ToString();
                user.SFoto = resultado.Rows[0]["foto_usua"].ToString();

                if ((resultado.Rows[0]["estado"].ToString()) == "True")
                {
                    switch (int.Parse(resultado.Rows[0]["rol_id"].ToString()))
                    {
                        case 1:
                            //Response.Redirect("Admin/AgregarAdministrador.aspx");
                            user.Url = "~/View/Admin/AgregarAdministrador.aspx";
                            break;

                        case 2:
                            //Response.Redirect("Profesor/ProfesorSubirNota.aspx");
                            user.Url = "~/View/Profesor/ProfesorSubirNota.aspx";
                            break;

                        case 3:
                            //Response.Redirect("Estudiante/EstudianteHorario.aspx");
                            user.Url = "~/View/Estudiante/EstudianteHorario.aspx";
                            break;

                        case 4:
                            //Response.Redirect("Acudiente/AcudienteBoletin.aspx");
                            user.Url = "~/View/Acudiente/AcudienteBoletin.aspx";
                            break;

                        default:
                            //Response.Redirect("Loggin.aspx");
                            user.Url = "~/View/Loggin.aspx";
                            break;
                    }

                }
                else
                {
                    //L_Error.Text = "Usuario Se Encuentra Inactivo";
                    user.Mensaje = "Usuario Se Encuentra Inactivo";
                    //Session["userId"] = null;
                    user.SUserId = null;
                }
            }
            else
            {
                //L_Error.Text = "Usuario Y/o Clave Incorrecto";
                user.Mensaje = "Usuario Y/o Clave Incorrecto";
                //Session["userId"] = null;
                user.SUserId = null;
            }
            return user;
        }



       
        /// ///////////////// AGREGAR ADMIN ///////////
       
        public UUser AgregarAdmin(
            int departamento,
            int ciudad, 
            string nombre,
            string apellido, 
            string direccion, 
            string telefono,
            string clave,
            string correo,
            FileUpload foto,
            int documento, 
            string usuario,
            int rol,
            string fechanac,
            string session) {



            UUser usua = new UUser();
            DUser dat = new DUser();


            int dep;
            dep = (departamento);

            int ciu;
            ciu = (ciudad);
            usua.Mensaje = "";
            if (departamento == 0 || ciudad == 0)
            {
                usua.Mensaje = "Debe seleccionar una opcion";
            }
            else
            {
                usua.Nombre = nombre;
                usua.Rol = Convert.ToString(rol);
                usua.UserName = usuario;
                usua.Clave = clave;
                usua.Correo = correo;
                usua.Apellido = apellido;
                usua.Direccion = direccion;
                usua.Telefono = telefono;
                usua.Documento = documento.ToString() ;
                usua.fecha_nacimiento = fechanac;
                usua.Departamento = Convert.ToString(dep);
                usua.Ciudad = Convert.ToString(ciu);
                usua.Session = session;
                usua.Foto = "jasdfvjasdbfsjadb.png";//cargarImagen(foto);


                if (usua.Foto != null)
                {
                    dat.insertarUsuarios(usua);
                    usua.Notificacion = "<script language='JavaScript'>window.alert('Administrador Insertado con Exito');</script>";
                    
                    usua.L_Aceptar1 = false;

                }
            }
            return usua;
        }

        public UUser agregarEstudiante(
            int departamento,
            int ciudad,
            string nombre,
            string apellido,
            string direccion,
            string telefono,
            string clave,
            string correo, 
            FileUpload foto,
            int documento,
            string usuario,
            int rol,
            string fechanac,
            string session,
            int id_acu
            )
        {
            UUser usua = new UUser();
            DUser dat = new DUser();


            int dep;
            dep = (departamento);

            int ciu;
            ciu = (ciudad);
            usua.Mensaje = "";
            if (departamento == 0 || ciudad == 0)
            {
                usua.Mensaje = "Debe seleccionar una opcion";
            }
            else
            {
                usua.Nombre = nombre;
                usua.Rol = Convert.ToString(rol);
                usua.UserName = usuario;
                usua.Clave = clave;
                usua.Correo = correo;
                usua.Apellido = apellido;
                usua.Direccion = direccion;
                usua.Telefono = telefono;
                usua.Documento = documento.ToString();
                usua.fecha_nacimiento = fechanac;
                usua.Departamento = Convert.ToString(dep);
                usua.Ciudad = Convert.ToString(ciu);
                usua.Session = session;
                usua.Foto = cargarImagen(foto);
                usua.id_Acudiente = Convert.ToString(id_acu);

                if (usua.Foto != null)
                {
                    dat.insertarEstudiante(usua);
                    //      this.Page.Response.Write("<script language='JavaScript'>window.alert('Administrador Insertado con Exito');</script>");

                    //      btn_AdministradorAceptar.Visible = false;

                }
            }
            return usua;
        }

        public UUser validarUser(string usuario,string documento)
        {
            UUser usua = new UUser();
            DUser dat = new DUser();

            usua.UserName = usuario;
            usua.Documento = documento.ToString();

            DataTable registros = dat.validar_usuarioadmin(usua);

            if (registros.Rows.Count > 0)
            {

                //tb_Vusuario.Text = Convert.ToString(registros.Rows[0]["user_name"].ToString());
                //tb_Vdocumento.Text = Convert.ToString(registros.Rows[0]["num_documento"].ToString());
                usua.Mensaje = "El Usuario ya existe";
                usua.L_Aceptar1 = false;
                usua.B_Botones1 = true;

            }
            else
            {
                //L_ErrorUsuario.Text = "";
                usua.Mensaje = "Usuario Disponible";
                //btn_DocenteAceptar.Visible = true;
                //btn_DocenteNuevo.Visible = true;
                //btn_validar.Visible = false;
                //tb_DocenteUsuario.ReadOnly = true;
                //tb_DocenteId.ReadOnly = true;
                usua.L_Aceptar1 = true;
                usua.B_Botones1 = false;

            }

            return usua;
        }

        public UUser buscarAcudiete(int departamento, int ciudad, String documento)
        {
            UUser usua = new UUser();
            DUser dat = new DUser();


            usua.Documento = documento.ToString();

            DataTable registros = dat.obtenerAcudiente(usua);

            if (departamento == 0 || ciudad == 0)
            {
                usua.Mensaje = "Debe seleccionar una opcion";
            }

            if (registros.Rows.Count > 0)
            {
                usua.Nombre = Convert.ToString(registros.Rows[0]["nombre_usua"].ToString());
                usua.Apellido = Convert.ToString(registros.Rows[0]["apellido_usua"].ToString());
                usua.id_Acudiente = Convert.ToString(registros.Rows[0]["id_usua"].ToString());

                usua.L_Aceptar1 = true;
                //tb_AcudienteNombre.ReadOnly = true;
                //tb_AcudienteId.ReadOnly = true;
                //tb_AcudienteApellido.ReadOnly = true;
                //L_ErrorAcudiente.Text = "";


                //tb_EstudianteNombre.ReadOnly = false;
                //tb_EstudianteApellido.ReadOnly = false;
                //tb_EstudianteId.ReadOnly = false;
                //tb_EstudianteDireccion.ReadOnly = false;
                //tb_EstudianteTelefono.ReadOnly = false;
                //tb_EstudianteUsuario.ReadOnly = false;
                //tb_EstudianteContrasenia.ReadOnly = false;
                //btnigm_calendar.Visible = true;
                //tb_EstudianteCorreo.ReadOnly = false;
            }
            else
            {


                usua.MensajeAcudiente = "El Acudiente No se encuentra en la base de Datos";

            }
            return usua;
        }


        protected String cargarImagen(FileUpload foto)
        {
            ClientScriptManager cm = this.ClientScript;
            String nombreArchivo = System.IO.Path.GetFileName(foto.PostedFile.FileName);
            String extension = System.IO.Path.GetExtension(foto.PostedFile.FileName);
            String saveLocation = "";

            if (!(string.Compare(extension, ".png", true) == 0 || string.Compare(extension, ".jpeg", true) == 0 || string.Compare(extension, ".jpg", true) == 0))
            {
                cm.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Solo se admiten imagenes en formato Jpeg o Gif');</script>");
                //btnigm_calendar.Visible = true;


                return null;
            }

            saveLocation = Server.MapPath("~/FotosUser") + "/" + nombreArchivo;

            if (System.IO.File.Exists(saveLocation))
            {
                cm.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Ya existe una imagen en el servidor con ese nombre');</script>");
                return null;
            }

            foto.PostedFile.SaveAs(saveLocation);
            cm.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('El archivo de imagen ha sido cargado');</script>");

            return "~/FotosUser" + "/" + nombreArchivo;
        }


        public UUser editarBuscarAdmin(int docenteid)
        {
            UUser usua = new UUser();
            DUser dat = new DUser();

            usua.Documento=docenteid.ToString();

            DataTable registros = dat.obtenerUsuarioMod(usua);

            if (registros.Rows.Count > 0)
            {
                usua.Nombre = Convert.ToString(registros.Rows[0]["nombre_usua"].ToString());
                usua.Apellido = Convert.ToString(registros.Rows[0]["apellido_usua"].ToString());
                //usua.Rol = Convert.ToString(registros.Rows[0]["rol_id"].ToString());
                usua.Estado = Convert.ToString(registros.Rows[0]["estado"].ToString());
                usua.Correo = Convert.ToString(registros.Rows[0]["correo"].ToString());
                usua.Direccion = Convert.ToString(registros.Rows[0]["direccion"].ToString());
                usua.Telefono = Convert.ToString(registros.Rows[0]["telefono"].ToString());
                usua.UserName = Convert.ToString(registros.Rows[0]["user_name"].ToString());
                usua.Clave = Convert.ToString(registros.Rows[0]["clave"].ToString());
                usua.fecha_nacimiento = Convert.ToString(registros.Rows[0]["fecha_nac"].ToString());
                usua.Departamento = Convert.ToString(registros.Rows[0]["dep_nacimiento"].ToString());
                usua.Ciudad = Convert.ToString(registros.Rows[0]["ciu_nacimiento"].ToString());
                usua.Notificacion = "<script language='JavaScript'>window.alert('Datos encontrados');</script>";
                ////this.Page.Response.Write("<script language='JavaScript'>window.alert('<<Listo men>>');</script>");

                //tb_DocenteId.ReadOnly = true;
                //tb_DocenteNombre.ReadOnly = false;
                //tb_DocenteApellido.ReadOnly = false;
                //tb_DocenteCorreo.ReadOnly = false;
                //tb_DocenteDireccion.ReadOnly = false;
                //tb_DocenteTelefono.ReadOnly = false;
                //tb_DocenteUsuario.ReadOnly = false;
                //tb_DocenteContrasenia.ReadOnly = false;
                //fechanac.ReadOnly = false;
                //L_ErrorAdmin.Text = "";

                usua.B_Botones1 = false;
                usua.L_Aceptar1 = true;
                //btn_DocenteAceptar.Visible = false;

            }
            else
            {
                usua.Notificacion = "<script language='JavaScript'>window.alert('<<Hubo prpblemas al traer los datos>>');</script>";
                usua.Mensaje = "Sin Registros";

            }
            return usua;
        }

        public UUser editarAdmin(
            int departamento,
            int ciudad,
            string nombre,
            string usuario,
            string clave,
            string correo,
            string apellido,
            string direccion,
            string telefono,
            int documento,
            //FileUpload foto,
            string fechanac,
            string session
            )
        {
            DUser dat = new DUser();
            UUser usua = new UUser();
            
            int rol = 2;
            String foto = "C://Hii";
            if (departamento == 0 || ciudad == 0)
            {
                usua.Mensaje = "Debe seleccionar una opcion";
            }
            else
            {
                usua.Nombre = nombre;
                usua.Rol = Convert.ToString(rol);
                usua.Ciudad = Convert.ToString(ciudad);
                usua.Departamento = Convert.ToString(departamento);
                usua.UserName = usuario;
                usua.Clave = clave;
                usua.Correo = correo;
                usua.Apellido = apellido;
                usua.Direccion = direccion;
                usua.Telefono = telefono;
                usua.Documento = Convert.ToString(documento);
                //usua.Foto = tb_AdministradorFoto.FileName;
                usua.Foto = foto;
                usua.Estado = true.ToString();
                usua.fecha_nacimiento = fechanac;
                usua.Session = session;


                DataTable registros = dat.EditarUsuario(usua);
                //this.Page.Response.Write("<script language='JavaScript'>window.alert('Docente Editado con Exito');</script>");
                usua.Notificacion = "<script language='JavaScript'>window.alert('Docente Editado con Exito');</script>";
            }

            return usua;
        }

        public UUser verificarCorreo(
            string destinatario,
            string userId,
            string persona,
            string apePersona,
            string correo_l
            )
        {
            DUser dat = new DUser();
            UUser usua = new UUser();

            DataTable resultado = dat.verificarCorreo(destinatario);

            if (resultado.Rows.Count > 0)
            {
                DaoUser dao = new DaoUser();
                mensaje = mensaje + "<br><br>Atentamente: " + persona + " " + apePersona + "<br>Correo para responder: " + correo_l + "";
                string cadena = mensaje;
                CorreoEnviar correo = new CorreoEnviar();
                correo.enviarCorreoEnviar(destinatario, asunto, mensaje);
                this.RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('Su Mensaje ha sido Enviado.');window.location=\"AdministradorMensaje.aspx\"</script>");
            }
            else
            {
                L_Verificar.Text = "El correo digitado no existe";
                TB_Destinatario.Text = "";
            }

            return usua;
        }


    }



}

