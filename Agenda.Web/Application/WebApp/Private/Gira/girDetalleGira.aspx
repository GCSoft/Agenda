<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girDetalleGira.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girDetalleGira" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>
    
    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Detalle de Gira"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Seleccione las opciones disponibles para complementar la información de la Gira."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlSubMenu" runat="server">
        
        <asp:Panel ID="DatosGiraPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="DatosGiraButton" ImageUrl="~/Include/Image/Icon/SubMenuDatosGenerales.png" runat="server" OnClick="DatosGiraButton_Click"></asp:ImageButton><br />
            Datos de la Gira
        </asp:Panel>

        <asp:Panel ID="ProgramaGiraPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ProgramaGiraButton" ImageUrl="~/Include/Image/Icon/SubMenuInformacionComplementaria.png" runat="server" OnClick="ProgramaGiraButton_Click"></asp:ImageButton><br />
            Programa
        </asp:Panel>

        <asp:Panel ID="ContactoPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="ContactoButton" ImageUrl="~/Include/Image/Icon/SubMenuContacto.png" runat="server" OnClick="ContactoButton_Click"></asp:ImageButton><br />
            Contactos de la Gira
        </asp:Panel>

        <asp:Panel ID="RechazarPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="RechazarButton" ImageUrl="~/Include/Image/Icon/SubMenuRechazo.png" runat="server" OnClick="RechazarButton_Click"></asp:ImageButton><br />
            Cancelar Gira
        </asp:Panel>

        <asp:Panel ID="CuadernilloGiraPanel" CssClass="IconoPanel" runat="server" Visible="true">
            <asp:ImageButton ID="CuadernilloGiraButton" ImageUrl="~/Include/Image/Icon/SubMenuGenerarCuadernillo.png" runat="server"></asp:ImageButton><br />
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
				    <td class="Especial">Nombre de la Gira</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblGiraNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Especial">Dependencia</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblDependenciaNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Fecha de la Gira</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblGiraFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Especial">Estatus</td>
				    <td class="Espacio"></td>
				    <td class="Campo" colspan="5"><asp:Label ID="lblEstatusGiraNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			    </tr>
                <tr>
				    <td class="Nombre">Detalle</td>
				    <td class="Espacio"></td>
				    <td class="Etiqueta" colspan="5"><asp:Label ID="lblGiraDetalle" runat="server"></asp:Label></td>
			    </tr>
			    <tr>
				    <td class="Nombre"></td>
				    <td class="Espacio"></td>
				    <td></td>
				    <td class="Espacio"></td>
				    <td class="Nombre"></td>
				    <td class="Espacio"></td>
				    <td></td>
			    </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlProgramaSection" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align:left;">
                        <asp:Label ID="lblProgramaSection" CssClass="Label_Detalle_Invitacion" runat="server" Text="Programa"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvPrograma" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
						    DataKeyNames="GiraConfiguracionId,EventoNombre" 
						    OnRowDataBound="gvPrograma_RowDataBound"
                            OnSorting="gvPrograma_Sorting">
                            <RowStyle CssClass="Grid_Row" />
                            <EditRowStyle Wrap="True" />
                            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
                            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
                            <EmptyDataTemplate>
                                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                    <tr class="Grid_Header">
                                        <td style="width:200px;">EventoNombre</td>
									    <td>LugarEventoNombre</td>
									    <td style="width:150px;">EventoFecha</td>
									    <td style="width:100px;">EventoHoraInicio</td>
                                        <td style="width:100px;">EventoHoraFin</td>
                                    </tr>
                                    <tr class="Grid_Row">
                                        <td colspan="5">No se ha capturado el programa de la gira</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <Columns>
							    <asp:BoundField HeaderText="EventoNombre"       ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="200px"	DataField="Nombre"                          SortExpression="Nombre"></asp:BoundField>
							    <asp:BoundField HeaderText="LugarEventoNombre"  ItemStyle-HorizontalAlign="Left"							DataField="Comentarios" HtmlEncode="false"  SortExpression="Comentarios"></asp:BoundField>
							    <asp:BoundField HeaderText="EventoFecha"        ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="Organizacion"                    SortExpression="Organizacion"></asp:BoundField>
							    <asp:BoundField HeaderText="EventoHoraInicio"   ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Telefono"                        SortExpression="Telefono"></asp:BoundField>
							    <asp:BoundField HeaderText="EventoHoraFin"      ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Email"                           SortExpression="Email"></asp:BoundField>
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
						    DataKeyNames="GiraContactoId,Nombre" 
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
                                        <td colspan="6">No se encontraron Contactos asociados a la gira</td>
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
    
    <asp:HiddenField ID="hddGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="hddEstatusGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />
    <asp:HiddenField ID="Expired" runat="server" Value="0" />
    <asp:HiddenField ID="Sender" runat="server" Value=""  />
	<asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
