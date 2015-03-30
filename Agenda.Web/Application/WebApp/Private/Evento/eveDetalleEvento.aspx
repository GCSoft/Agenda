<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveDetalleEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveDetalleEvento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">
    
    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>
    
    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Detalle de Evento"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Seleccione las opciones disponibles para complementar la información del evento."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlSubMenu" runat="server">

        <asp:Panel ID="EliminarRepresentantePanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="EliminarRepresentanteButton" ImageUrl="~/Include/Image/Icon/SubMenuEliminarRepresentante.png" runat="server" OnClick="EliminarRepresentanteButton_Click" ></asp:ImageButton><br />
            Eliminar representante
        </asp:Panel>
        
        <asp:Panel ID="DatosGeneralesPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="InformacionGeneralButton" ImageUrl="~/Include/Image/Icon/SubMenuDatosGenerales.png" runat="server" OnClick="InformacionGeneralButton_Click"></asp:ImageButton><br />
            Datos generales
        </asp:Panel>

        <asp:Panel ID="DatosEventoPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="DatosEventoButton" ImageUrl="~/Include/Image/Icon/SubMenuDatosEvento.png" runat="server" OnClick="DatosEventoButton_Click"></asp:ImageButton><br />
            Datos del evento
        </asp:Panel>

        <asp:Panel ID="ProgramaLogisticaPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ProgramaLogisticaButton" ImageUrl="~/Include/Image/Icon/SubMenuInformacionComplementaria.png" runat="server" OnClick="ProgramaLogisticaButton_Click"></asp:ImageButton><br />
            Programa
        </asp:Panel>

        <asp:Panel ID="ProgramaProtocoloPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ProgramaProtocoloButton" ImageUrl="~/Include/Image/Icon/SubMenuInformacionComplementaria.png" runat="server" OnClick="ProgramaProtocoloButton_Click"></asp:ImageButton><br />
            Programa
        </asp:Panel>

        <asp:Panel ID="ContactoPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ContactoButton" ImageUrl="~/Include/Image/Icon/SubMenuContacto.png" runat="server" OnClick="ContactoButton_Click"></asp:ImageButton><br />
            Contactos del evento
        </asp:Panel>

        <asp:Panel ID="AdjuntarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="AdjuntarButton" ImageUrl="~/Include/Image/Icon/SubMenuAdjuntarDocumento.png" runat="server" OnClick="AdjuntarButton_Click"></asp:ImageButton><br />
            Anexar documentos
        </asp:Panel>

        <asp:Panel ID="RechazarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="RechazarButton" ImageUrl="~/Include/Image/Icon/SubMenuRechazo.png" runat="server" OnClick="RechazarButton_Click"></asp:ImageButton><br />
            Cancelar evento
        </asp:Panel>

        <asp:Panel ID="CuadernilloLogisticaPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="CuadernilloLogisticaButton" ImageUrl="~/Include/Image/Icon/SubMenuGenerarCuadernillo.png" runat="server"></asp:ImageButton><br />
            Generar cuadernillo
        </asp:Panel>

        <asp:Panel ID="CuadernilloProtocoloPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="CuadernilloProtocoloButton" ImageUrl="~/Include/Image/Icon/SubMenuGenerarCuadernillo.png" runat="server"></asp:ImageButton><br />
            Generar cuadernillo
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
				    <td class="Especial">Dependencia</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblDependenciaNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Nombre de evento</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Fecha de evento</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Estatus</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEstatusEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Especial">Tipo de Cita</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblCategoriaNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
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
						    DataKeyNames="EventoContactoId,Nombre" 
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
                                        <td colspan="3">No se encontraron Contactos asociados al evento</td>
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

    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>
    
    <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="hddEstatusEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />
    <asp:HiddenField ID="Expired" runat="server" Value="0" />
    <asp:HiddenField ID="Logistica" runat="server" Value="0" />
    <asp:HiddenField ID="Sender" runat="server" Value=""  />
	<asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
