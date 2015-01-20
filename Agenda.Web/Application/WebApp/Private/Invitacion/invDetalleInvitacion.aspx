<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invDetalleInvitacion.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invDetalleInvitacion" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Detalle de Invitación"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Seleccione las opciones disponibles para complementar la información de la invitación."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlSubMenu" runat="server">
        
        <asp:Panel ID="DatosGeneralesPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="InformacionGeneralButton" ImageUrl="~/Include/Image/Icon/SubMenuDatosGenerales.png" runat="server" OnClick="InformacionGeneralButton_Click"></asp:ImageButton><br />
            Datos generales
        </asp:Panel>

        <asp:Panel ID="DatosEventoPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="DatosEventoButton" ImageUrl="~/Include/Image/Icon/SubMenuDatosEvento.png" runat="server" OnClick="DatosEventoButton_Click"></asp:ImageButton><br />
            Datos del evento
        </asp:Panel>

        <asp:Panel ID="ContactoPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ContactoButton" ImageUrl="~/Include/Image/Icon/SubMenuContacto.png" runat="server" OnClick="ContactoButton_Click"></asp:ImageButton><br />
            Contactos de la invitación
        </asp:Panel>

        <asp:Panel ID="FuncionarioPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="FuncionarioButton" ImageUrl="~/Include/Image/Icon/SubMenuAsociarFuncionario.png" runat="server" OnClick="FuncionarioButton_Click"></asp:ImageButton><br />
            Funcionarios asociados
        </asp:Panel>

        <asp:Panel ID="AdjuntarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="AdjuntarButton" ImageUrl="~/Include/Image/Icon/SubMenuAdjuntarDocumento.png" runat="server" OnClick="AdjuntarButton_Click"></asp:ImageButton><br />
            Anexar documentos
        </asp:Panel>

        <asp:Panel ID="RechazarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="RechazarButton" ImageUrl="~/Include/Image/Icon/SubMenuRechazo.png" runat="server" OnClick="RechazarButton_Click"></asp:ImageButton><br />
            Declinar invitación
        </asp:Panel>

        <asp:Panel ID="AprobarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="AprobarButton" ImageUrl="~/Include/Image/Icon/SubMenuAprobar.png" runat="server" OnClick="AprobarButton_Click"></asp:ImageButton><br />
            Aprobar invitación
        </asp:Panel>

        <asp:Panel ID="Historial" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="HistorialButton" ImageUrl="~/Include/Image/Icon/SubMenuHistorial.png" runat="server" OnClick="HistorialButton_Click"></asp:ImageButton><br />
            Historial de cambios
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnlCanvas" runat="server">
        
        <asp:Panel ID="pnlCaratulaSection" runat="server">
            <table class="InvitacionTable">
			    <tr>
				    <td class="Especial">Nombre de evento</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text="0"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Fecha de evento</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text="0"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Estatus</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEstatusInvitacionNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text="0"></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Especial">Tipo de Cita</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblCategoriaNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text="0"></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre">Conducto</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta"><asp:Label ID="lblConductoNombre" runat="server" Text=""></asp:Label></td>
				    <td class="Espacio"></td>
				    <td class="Nombre">Prioridad</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta"><asp:Label ID="lblPrioridadNombre" runat="server" Text=""></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre">Secretario Ramo</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblSecretarioRamoNombre" runat="server"></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre">Secretario Responsable</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblSecretarioResponsable" runat="server"></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre">Representante</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblSecretarioRepresentante" runat="server"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Nombre">Lugar del evento</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblLugarEventoCompleto" runat="server"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Nombre">Detalle del evento</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblEventoDetalle" runat="server"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Nombre"><asp:Label ID="lblObservacionesGenerales" runat="server" Text="Observaciones Generales"></asp:Label></td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblObservacionesGeneralesDetalle" runat="server"></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre"><asp:Label ID="lblMotivoRechazo" runat="server" Text="Motivo de Rechazo"></asp:Label></td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblMotivoRechazoDetalle" runat="server"></asp:Label></td>
			    </tr>
			    <tr style="height:10px;"><td colspan="7"></td></tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlFuncionariosSection" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:left;">
                        <asp:Label ID="lblFuncionariosSection" CssClass="Label_Detalle_Invitacion" runat="server" Text="Funcionarios asociados"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvFuncionario" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
						    DataKeyNames="UsuarioId,Nombre" 
						    OnRowDataBound="gvFuncionario_RowDataBound"
                            OnSorting="gvFuncionario_Sorting">
                            <RowStyle CssClass="Grid_Row" />
                            <EditRowStyle Wrap="True" />
                            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
                            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
                            <EmptyDataTemplate>
                                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                    <tr class="Grid_Header">
                                        <td style="width:150px;">Correo</td>
									    <td style="width:200px;">Puesto</td>
									    <td>Nombre</td>
                                    </tr>
                                    <tr class="Grid_Row">
                                        <td colspan="3">No se encontraron Funcionarios asociados a la invitación</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <Columns>
							    <asp:BoundField HeaderText="Correo" ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="150px"	DataField="Email"                   SortExpression="Email"></asp:BoundField>
							    <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="200px"	DataField="Puesto"                  SortExpression="Puesto"></asp:BoundField>
							    <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"							DataField="NombreCompletoTitulo"    SortExpression="NombreCompletoTitulo"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br /><br />
        </asp:Panel>

        <asp:Panel ID="pnlContactosSection" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:left;">
                        <asp:Label ID="lblContactosSection" CssClass="Label_Detalle_Invitacion" runat="server" Text="Contactos vinculados"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvContacto" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
						    DataKeyNames="InvitacionContactoId,Nombre" 
						    OnRowDataBound="gvContacto_RowDataBound"
                            OnSorting="gvContacto_Sorting">
                            <RowStyle CssClass="Grid_Row" />
                            <EditRowStyle Wrap="True" />
                            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
                            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
                            <EmptyDataTemplate>
                                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                    <tr class="Grid_Header">
                                        <td style="width:200px;">Nombre</td>
									    <td style="width:150px;">Puesto</td>
									    <td style="width:150px;">Organización</td>
									    <td style="width:100px;">Teléfono</td>
                                        <td style="width:100px;">Correo</td>
									    <td>Comentarios</td>
                                    </tr>
                                    <tr class="Grid_Row">
                                        <td colspan="3">No se encontraron Contactos asociados a la invitación</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <Columns>
							    <asp:BoundField HeaderText="Nombre"         ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="200px"	DataField="Nombre"                          SortExpression="Nombre"></asp:BoundField>
							    <asp:BoundField HeaderText="Puesto"         ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="Puesto"                          SortExpression="Puesto"></asp:BoundField>
							    <asp:BoundField HeaderText="Organización"   ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="Organizacion"                    SortExpression="Organizacion"></asp:BoundField>
							    <asp:BoundField HeaderText="Teléfono"		ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Telefono"                        SortExpression="Telefono"></asp:BoundField>
							    <asp:BoundField HeaderText="Correo"         ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Email"                           SortExpression="Email"></asp:BoundField>
							    <asp:BoundField HeaderText="Comentarios"    ItemStyle-HorizontalAlign="Left"							DataField="Comentarios" HtmlEncode="false"  SortExpression="Comentarios"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br /><br />
        </asp:Panel>

        <asp:Panel ID="pnlDocumentosSection" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:left;">
                        <asp:Label ID="lblDocumentosSection" CssClass="Label_Detalle_Invitacion" runat="server" Text="Documentos adjuntos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="DocumentoListPanel">
                            <asp:DataList ID="dlstDocumentoList" runat="server" CellPadding="5" CellSpacing="5" HorizontalAlign="Left" RepeatDirection="Horizontal" RepeatLayout="Table" OnItemDataBound="dlstDocumentoList_ItemDataBound" >
                                <ItemTemplate>
                                    <div class="Item">
                                        <asp:Image ID="DocumentoImage" runat="server" />
                                        <br />
                                        <asp:Label ID="DocumentoLabel" runat="server" CssClass="Texto" Text="Nombre del documento"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="SinDocumentoLabel" runat="server" CssClass="Texto" Text=""></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <br /><br />
        </asp:Panel>

        <asp:Panel ID="pnlComentariosSection" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:left;">
                        <asp:Label ID="lblComentariosSection" CssClass="Label_Detalle_Invitacion" runat="server" Text="Evaluaciones"></asp:Label>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkAgregarComentario" runat="server" CssClass="LinkButton_Regular" Text="Emitir Evaluación" OnClick="lnkAgregarComentario_Click" Visible="false"></asp:LinkButton>
                        <asp:LinkButton ID="lnkEditarComentario" runat="server" CssClass="LinkButton_Regular" Text="Editar Evaluación" OnClick="lnkEditarComentario_Click" Visible="false" ></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="ComentarioInvitacionDiv">
                            <div class="TituloDiv"><asp:Label ID="ComentarioTituloLabel" runat="server" Text=""></asp:Label></div>
                            <asp:Repeater ID="repComentarios" runat="server">
                                <HeaderTemplate>
                                    <table border="1" class="ComentarioInvitacionTable">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="Numero_<%# DataBinder.Eval(Container.DataItem, "RespuestaEvaluacionId") %>">
							                <%# DataBinder.Eval(Container.DataItem, "iRow") %>
						                </td>
                                        <td>
							                <table class="ComentarioInvitacionTable">
								                <tr>
									                <td class="Nombre">
										                <%# DataBinder.Eval(Container.DataItem, "UsuarioNombre")%>
                                                        <asp:HiddenField ID="hddUsuarioId_Comentario" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UsuarioId")%>' />
									                </td>
                                                    <td class="Fecha">
										                <%# DataBinder.Eval(Container.DataItem, "FechaModificacion")%>
									                </td>
                                                </tr>
                                                <tr>
                                                    <td class="Texto" colspan="2">
										                <%# DataBinder.Eval(Container.DataItem, "ComentarioFull")%>
									                </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:Label ID="SinComentariosLabel" runat="server" CssClass="Texto" Text=""></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <br /><br />
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>
    
    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" style="margin-top:-250px; margin-left:-400px;" Height="500px" Width="800px">
            <asp:Panel ID="pnlPopUpHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Click"></asp:ImageButton></td>
                        <td class="Espacio"></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">¿Es conveniente que atienda el C. Gobernador?</td>
                    </tr>
                    <tr>
                        <td class="Contenedor">
                            <asp:RadioButtonList ID="rblRespuestaEvaluacion" runat="server" RepeatDirection="Horizontal" style="cursor:pointer;" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="rblRespuestaEvaluacion_SelectedIndexChanged">
					        </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="height:10px;"><td></td></tr>
                    <tr>
                        <td class="Etiqueta">En caso de ser "NO", proponga un funcionario que pueda representar al títular del Ejecutivo</td>
                    </tr>
                    <tr>
                        <td class="Contenedor">
                            <table style="border:0px; padding:0px; width:100%;">
                                <tr>
                                    <td style="text-align:left; width:80px;">
                                        Nombre:
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPopUpNombre" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        Cargo:
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPopUpCargo" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        Tel. Oficina:
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPopUpTelefonoOficina" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="50" Width="15%"></asp:TextBox>&nbsp;&nbsp;
                                        Celular:&nbsp;<asp:TextBox ID="txtPopUpTelefonoCelular" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="50" Width="15%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        Particular:&nbsp;<asp:TextBox ID="txtPopUpTelefonoParticular" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="50" Width="15%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        Otro:&nbsp;<asp:TextBox ID="txtPopUpTelefonoOtro" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="50" Width="15%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height:10px;"><td></td></tr>
                    <tr>
                        <td class="Etiqueta">Comentarios:</td>
                    </tr>
                    <tr>
                        <td>
                            <CKEditor:CKEditorControl ID="ckePopUpComentario" BasePath="~/Include/Components/CKEditor/Core/" runat="server" Height="90px"></CKEditor:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones">
                            <asp:Button ID="btnPopUpCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUpCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPopUpMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <br /><br />
    
    <asp:HiddenField ID="hddInvitacionId" runat="server" Value="0" />
    <asp:HiddenField ID="hddEstatusInvitacionId" runat="server" Value="0" />
    <asp:HiddenField ID="hddInvitacionComentarioId" runat="server" Value="0" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />
    <asp:HiddenField ID="Sender" runat="server" Value=""  />
	<asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
