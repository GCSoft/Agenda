<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girConfiguracionGira.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girConfiguracionGira" %>
<%@ Register Src="~/Include/WebUserControls/wucTimer.ascx" TagPrefix="wuc" TagName="wucTimer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
    <script type = "text/javascript">

        function ColoniaSelected_LugarEvento(sender, e) {
            $get("<%=hddPopUpColoniaId_LugarEvento.ClientID %>").value = e.get_value();
        }

        function LugarEventoSelected(sender, e) {

            // Valor seleccionado
            $get("<%=hddLugarEventoId.ClientID %>").value = e.get_value();

            // Provocar PostBack
            __doPostBack("<%= hddLugarEventoId.ClientID %>", "");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Programa"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Se presentan los cambios realizados al Gira."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">

        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"></td>
                <td></td>
			</tr>
        </table>

    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnTrasladoVehiculo" runat="server" Text="Traslado en vehículo" CssClass="Button_General" width="175px" onclick="btnTrasladoVehiculo_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnTrasladoHelicoptero" runat="server" Text="Traslado en helicóptero" CssClass="Button_General" width="175px" onclick="btnTrasladoHelicoptero_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnEvento" runat="server" Text="Evento" CssClass="Button_General" width="175px" onclick="btnEvento_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnActividadGeneral" runat="server" Text="Actividad General" CssClass="Button_General" width="175px" onclick="btnActividadGeneral_Click" /> &nbsp;&nbsp;
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvPrograma" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
			DataKeyNames="GiraConfiguracionId,TipoGiraConfiguracionId,ConfiguracionDetalle" 
            OnRowCommand="gvPrograma_RowCommand"
			OnRowDataBound="gvPrograma_RowDataBound"
            OnSorting="gvPrograma_Sorting">
            <RowStyle CssClass="Grid_Row" />
            <EditRowStyle Wrap="True" />
            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width:150px;">Grupo</td>
						<td style="width:100px;">Fecha</td>
						<td style="width:100px;">Inicio</td>
                        <td style="width:100px;">Fin</td>
                        <td>Detalle</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="5">No se ha capturado el programa de la gira</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
				<asp:BoundField HeaderText="Grupo"      ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="ConfiguracionGrupo"                      SortExpression="ConfiguracionGrupo"></asp:BoundField>
				<asp:BoundField HeaderText="Fecha"      ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="GiraFechaEstandar"                       SortExpression="GiraFechaEstandar"></asp:BoundField>
				<asp:BoundField HeaderText="Inicio"     ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="ConfiguracionHoraInicioEstandar"         SortExpression="ConfiguracionHoraInicioEstandar"></asp:BoundField>
				<asp:BoundField HeaderText="Fin"        ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="ConfiguracionHoraFinEstandar"            SortExpression="ConfiguracionHoraFinEstandar"></asp:BoundField>
                <asp:BoundField HeaderText="Detalle"    ItemStyle-HorizontalAlign="Left"							DataField="ConfiguracionDetalle" HtmlEncode="false" SortExpression="ConfiguracionDetalle"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" OnClientClick="return confirm('¿Seguro que desea eliminar la partida?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br /><br />
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_TrasladoVehiculo" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_TrasladoVehiculoContent" runat="server" CssClass="PopUpContent" style="margin-top:-130px; margin-left:-260px;" Height="260px" Width="520px">
            <asp:Panel ID="pnlPopUp_TrasladoVehiculoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_TrasladoVehiculoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_TrasladoVehiculo" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_TrasladoVehiculo_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_TrasladoVehiculoBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                        <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                        <td class="Campo">
                            <table style="width:405px">
	                            <tr>
		                            <td colspan="2" style="text-align:left;">
                                        <asp:DropDownList ID="ddlAgrupacion_TrasladoVehiculo" runat="server" CssClass="DropDownList_General" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupacion_TrasladoVehiculo_SelectedIndexChanged"></asp:DropDownList>
		                            </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtOtraAgrupacion_TrasladoVehiculo" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="360px"></asp:TextBox>
		                            </td>
                                    <td style="text-align:right;">
                                        <asp:Button ID="btnNuevaAgrupacion_TrasladoVehiculo" runat="server" Text="+" CssClass="Button_Special_Gray" Enabled="false" ToolTip="Nueva Agrupacion" Width="25px" OnClick="btnNuevaAgrupacion_TrasladoVehiculo_Click" />
		                            </td>
	                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoVehiculoDetalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
				        <td class="Etiqueta">Desde las</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo">
                            <wuc:wucTimer ID="wucPopUp_TrasladoVehiculoTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                            <wuc:wucTimer ID="wucPopUp_TrasladoVehiculoTimerHasta" runat="server" />
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_TrasladoVehiculoCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_TrasladoVehiculoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_TrasladoVehiculoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_TrasladoHelicoptero" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_TrasladoHelicopteroContent" runat="server" CssClass="PopUpContent" style="margin-top:-210px; margin-left:-260px;" Height="420px" Width="580px">
            <asp:Panel ID="pnlPopUp_TrasladoHelicopteroHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_TrasladoHelicopteroTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_TrasladoHelicoptero" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_TrasladoHelicoptero_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_TrasladoHelicopteroBody" runat="server" CssClass="PopUpBody">
                <asp:TabContainer ID="tabFormulario_TrasladoHelicoptero" runat="server" ActiveTabIndex="0" CssClass="Tabcontainer_General" Height="280" >
                    <asp:TabPanel ID="tpnlGenerales_TrasladoHelicoptero" runat="server">
                        <HeaderTemplate>Datos generales</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
                                    <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                                    <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                                    <td class="Campo">
                                        <table style="width:405px">
	                                        <tr>
		                                        <td colspan="2" style="text-align:left;">
                                                    <asp:DropDownList ID="ddlAgrupacion_TrasladoHelicoptero" runat="server" CssClass="DropDownList_General" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupacion_TrasladoHelicoptero_SelectedIndexChanged"></asp:DropDownList>
		                                        </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtOtraAgrupacion_TrasladoHelicoptero" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="360px"></asp:TextBox>
		                                        </td>
                                                <td style="text-align:right;">
                                                    <asp:Button ID="btnNuevaAgrupacion_TrasladoHelicoptero" runat="server" Text="+" CssClass="Button_Special_Gray" Enabled="false" ToolTip="Nueva Agrupacion" Width="25px" OnClick="btnNuevaAgrupacion_TrasladoHelicoptero_Click" />
		                                        </td>
	                                        </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Nombre</td>
                                    <td class="VinetaObligatorio">*</td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroDetalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta">Desde las</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo">
                                        <wuc:wucTimer ID="wucPopUp_TrasladoHelicopteroTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                                        <wuc:wucTimer ID="wucPopUp_TrasladoHelicopteroTimerHasta" runat="server" />
				                    </td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td class="Etiqueta">Lugar del Helipuerto</td>
                                    <td class="VinetaObligatorio">*</td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroLugar" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Domicilio del Helipuerto</td>
                                    <td class="VinetaObligatorio">*</td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroDomicilio" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Coordenadas del Helipuerto</td>
                                    <td class="VinetaObligatorio">*</td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroCoordenadas" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlComite_TrasladoHelicoptero" runat="server">
                        <HeaderTemplate>Comité de recepción en el Helipuerto</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
                                    <td class="Etiqueta">Nombre</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroComiteNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Puesto</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroComitePuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta"><asp:Button ID="btnAgregarComiteHelipuerto" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarComiteHelipuerto_Click" /></td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"></td>
                                    <td></td>
			                    </tr>
                                <tr><td colspan="4" style="height:10px;"></td></tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="border:0px solid #000000; clear:both; position:relative; width:100%;">
                                            <table cellspacing="0" rules="all" border="1" style="border-collapse:collapse; width:550px;">
                                                <tr class="Grid_Header_PopUp">
                                                    <th scope="col" style="width:80px;">Orden</th>
                                                    <th scope="col" style="width:200px;">Nombre</th>
										            <th scope="col" style="width:270px;">Puesto</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border:1px solid #C1C1C1; height:100px; overflow-x:hidden; overflow-y:scroll; text-align:left; Width:548px">
                                            <asp:GridView ID="gvComiteHelipuerto" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" ShowHeader="false" Width="100%"
                                                DataKeyNames="Orden,Nombre,Puesto"
                                                OnRowCommand="gvComiteHelipuerto_RowCommand"
                                                OnRowDataBound="gvComiteHelipuerto_RowDataBound"
                                                OnSorting="gvComiteHelipuerto_Sorting">
                                                <RowStyle CssClass="Grid_Row_Scroll" />
                                                <EmptyDataTemplate>
                                                    <div style="border:0px; clear:both; color:#675C9D; font:11px Tahoma; font-weight:normal; height:15px; position:relative; text-align:center; width:100%;">
                                                        No se ha capturado el comité de recepción en el helipuerto
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="78px"  DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="194px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Botones">
                            <asp:Button ID="btnPopUp_TrasladoHelicopteroCommand" runat="server" Text="" CssClass="Button_General" Width="200px" OnClick="btnPopUp_TrasladoHelicopteroCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes">
                            <asp:Label ID="lblPopUp_TrasladoHelicopteroMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_Evento" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_EventoContent" runat="server" CssClass="PopUpContent" style="margin-top:-310px; margin-left:-375px;" Height="620px" Width="650px">
            <asp:Panel ID="pnlPopUp_EventoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_EventoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_Evento" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Evento_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_EventoBody" runat="server" CssClass="PopUpBody">
                <asp:TabContainer ID="tabFormulario_Evento" runat="server" ActiveTabIndex="0" CssClass="Tabcontainer_General" Height="485" >
                    <asp:TabPanel ID="tpnlDetalle_Evento" runat="server">
                        <HeaderTemplate>Datos del Evento</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
                                    <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                                    <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                                    <td class="Campo">
                                        <table style="width:405px">
	                                        <tr>
		                                        <td colspan="2" style="text-align:left;">
                                                    <asp:DropDownList ID="ddlAgrupacion_Evento" runat="server" CssClass="DropDownList_General" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupacion_Evento_SelectedIndexChanged"></asp:DropDownList>
		                                        </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtOtraAgrupacion_Evento" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="360px"></asp:TextBox>
		                                        </td>
                                                <td style="text-align:right;">
                                                    <asp:Button ID="btnNuevaAgrupacion_Evento" runat="server" Text="+" CssClass="Button_Special_Gray" Enabled="false" ToolTip="Nueva Agrupacion" Width="25px" OnClick="btnNuevaAgrupacion_Evento_Click" />
		                                        </td>
	                                        </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Nombre</td>
                                    <td class="VinetaObligatorio">*</td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoDetalle" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="1000" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta">Desde las</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo">
                                        <wuc:wucTimer ID="wucPopUp_EventoTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                                        <wuc:wucTimer ID="wucPopUp_EventoTimerHasta" runat="server" />
				                    </td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Lugar del evento</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo">

								        <table style="border:0px; padding:0px; width:100%;">
                                            <tr>
                                                <td style="text-align:left; width:520px;">
                                                    <asp:TextBox ID="txtPopUp_EventoLugar" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                                                </td>
                                                <td style="text-align:left; width:30px;">
                                                    <asp:Button ID="btnNuevoLugarEvento" runat="server" Text="+" CssClass="Button_Special_Blue" ToolTip="Nuevo Lugar de evento" Width="25px" OnClick="btnNuevoLugarEvento_Click" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
								        <asp:HiddenField ID="hddLugarEventoId" runat="server" OnValueChanged="hddLugarEventoId_ValueChanged" />
								        <asp:AutoCompleteExtender
									        ID="autosuggestLugarEvento" 
									        runat="server"
									        TargetControlID="txtPopUp_EventoLugar"
									        ServiceMethod="WSLugarEvento"
                                            ServicePath=""
									        CompletionInterval="100"
                                            DelimiterCharacters=""
                                            Enabled="True"
									        EnableCaching="False"
									        MinimumPrefixLength="2"
									        OnClientItemSelected="LugarEventoSelected"
									        CompletionListCssClass="Autocomplete_CompletionListElement"
									        CompletionListItemCssClass="Autocomplete_ListItem"
									        CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                                        />

				                    </td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Domicilio</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"><asp:TextBox ID="txtPopUp_Domicilio" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="400px"></asp:TextBox></td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td class="Etiqueta">Lugar de arribo</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoLugarArribo" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta">Medio de traslado</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo"><asp:CheckBoxList ID="chklPopUp_EventoMedioTraslado" runat="server" CssClass="CheckBox_Regular" RepeatDirection="Horizontal" ></asp:CheckBoxList></td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td class="Etiqueta">Tipo de montaje</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoTipoMontaje" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta">Aforo</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoAforo" runat="server" CssClass="Textbox_General" MaxLength="5" Text="0" Width="130px"></asp:TextBox></td>
                                    <td></td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlGenerales_Evento" runat="server">
                        <HeaderTemplate>Datos generales</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
				                    <td class="Etiqueta">Caracteristicas de invitados</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2"><asp:TextBox ID="txtPopUp_EventoCaracteristicasInvitados" runat="server" CssClass="Textarea_General" Height="50px" MaxLength="500" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Esposa</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2">
                                        <table style="border:0px; border-spacing:0px; width:100%;">
                                            <tr>
                                                <td style="text-align:left; width:150px;">
                                                    <asp:CheckBox ID="chkPopUp_EventoEsposaInvitada" runat="server" CssClass="CheckBox_Regular" Text="Invitada" AutoPostBack="True" OnCheckedChanged="chkPopUp_EventoEsposaInvitada_CheckedChanged" />
                                                </td>
                                                <td style="text-align:left; width:70px;">
                                                    Asiste:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:RadioButtonList ID="rblPopUp_EventoConfirmacionEsposa" runat="server" RepeatDirection="Horizontal" Enabled ="false">
						                                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
						                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Pendiente" Value="3"></asp:ListItem>
					                                </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
				                    </td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Medios de comunicación</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo"><asp:DropDownList ID="ddlPopUp_EventoMedioComunicacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Tipo de vestimenta</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo"><asp:DropDownList ID="ddlPopUp_EventoTipoVestimenta" runat="server" CssClass="DropDownList_General" Width="216px" AutoPostBack="True" OnSelectedIndexChanged="ddlPopUp_EventoTipoVestimenta_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta"></td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoTipoVestimentaOtro" runat="server" CssClass="Textbox_Disabled" MaxLength="1000" Width="400px" Enabled="false"></asp:TextBox></td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Menú</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2"><asp:TextBox ID="txtPopUp_EventoMenu" runat="server" CssClass="Textarea_General" Height="50px" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Pronóstico</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoPronostico" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                    <td></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Acción a realizar</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo" colspan="2"><asp:TextBox ID="txtPopUp_EventoAccionRealizar" runat="server" CssClass="Textarea_General" Height="50px" MaxLength="200" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Incluir nota/observación</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2">
                                        <asp:RadioButtonList ID="rblPopUp_EventoIncluirNotaEvento" runat="server" RepeatDirection="Horizontal">
						                    <asp:ListItem Text="No Incluir" Value="1" Selected="True"></asp:ListItem>
						                    <asp:ListItem Text="Al Inicio de la sección" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Al Final de la sección" Value="3"></asp:ListItem>
					                    </asp:RadioButtonList>
				                    </td>
			                    </tr>
                                <tr>
				                    <td colspan="4"><asp:TextBox ID="txtPopUp_EventoNotaEvento" runat="server" CssClass="Textarea_General" Height="60px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlComite_Evento" runat="server">
                        <HeaderTemplate>Comité de recepción</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
                                    <td class="Etiqueta">Nombre</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoComiteNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Puesto</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoComitePuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta"><asp:Button ID="btnAgregarComite" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarComite_Click" /></td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"></td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="border:0px solid #000000; clear:both; position:relative; width:100%;">
                                            <table cellspacing="0" rules="all" border="1" style="border-collapse:collapse; width:620px;">
                                                <tr class="Grid_Header_PopUp">
                                                    <th scope="col" style="width:80px;">Orden</th>
                                                    <th scope="col" style="width:200px;">Nombre</th>
										            <th scope="col">Puesto</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border:1px solid #C1C1C1; height:220px; overflow-x:hidden; overflow-y:scroll; text-align:left; Width:618px">
                                            <asp:GridView ID="gvComite" runat="server" AllowPaging="false" AllowSorting="False" AutoGenerateColumns="False" ShowHeader="false" Width="100%"
                                                DataKeyNames="Orden,Nombre,Puesto"
                                                OnRowCommand="gvComite_RowCommand"
                                                OnRowDataBound="gvComite_RowDataBound">
                                                <RowStyle CssClass="Grid_Row_Scroll" />
                                                <EmptyDataTemplate>
                                                    <div style="border:0px; clear:both; color:#675C9D; font:11px Tahoma; font-weight:normal; height:15px; position:relative; text-align:center; width:100%;">
                                                        No se ha capturado el comité de recepción
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="79px"  DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Incluir nota/observación</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2">
                                        <asp:RadioButtonList ID="rblPopUp_EventoIncluirNotaComite" runat="server" RepeatDirection="Horizontal">
						                    <asp:ListItem Text="No Incluir" Value="1" Selected="True"></asp:ListItem>
						                    <asp:ListItem Text="Al Inicio de la sección" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Al Final de la sección" Value="3"></asp:ListItem>
					                    </asp:RadioButtonList>
				                    </td>
			                    </tr>
                                <tr>
				                   <td colspan="4"><asp:TextBox ID="txtPopUp_EventoNotaEventoComite" runat="server" CssClass="Textarea_General" Height="60px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlOrdenDia_Evento" runat="server">
                        <HeaderTemplate>Orden del día</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                               <tr>
                                    <td class="Campo" colspan="4">
                                        <table style="width:100%; padding:0px; border-spacing:0px;">
                                            <tr>
                                                <td style="width:50px;">
                                                    Detalle
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrdenDiaDetalle" runat="server" CssClass="Textarea_General" Height="50px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
								</tr>
								<tr>
									<td class="Etiqueta"><asp:Button ID="btnAgregarOrdenDia" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarOrdenDia_Click" /></td>
									<td class="Espacio"></td>
									<td class="Campo"></td>
									<td></td>
								</tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="border:0px solid #000000; clear:both; position:relative; width:100%;">
                                            <table cellspacing="0" rules="all" border="1" style="border-collapse:collapse; width:620px;">
                                                <tr class="Grid_Header_PopUp">
                                                    <th scope="col" style="width:102px;">Orden</th>
                                                    <th scope="col">Detalle</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border:1px solid #C1C1C1; height:210px; overflow-x:hidden; overflow-y:scroll; text-align:left; Width:618px">
                                            <asp:GridView ID="gvOrdenDia" runat="server" AllowPaging="false" AllowSorting="False" AutoGenerateColumns="False" ShowHeader="false" Width="100%"
                                                DataKeyNames="Orden,Detalle"
												OnRowCommand="gvOrdenDia_RowCommand"
												OnRowDataBound="gvOrdenDia_RowDataBound">
                                                <RowStyle CssClass="Grid_Row_Scroll" />
                                                <EmptyDataTemplate>
                                                    <div style="border:0px; clear:both; color:#675C9D; font:11px Tahoma; font-weight:normal; height:15px; position:relative; text-align:center; width:100%;">
                                                        No se ha capturado la Orden del Día
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
													<asp:BoundField HeaderText="Orden"      ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="101px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
													<asp:BoundField HeaderText="Detalle"    ItemStyle-HorizontalAlign="Left"                            DataField="Detalle" SortExpression="Detalle"></asp:BoundField>
													<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
														<ItemTemplate>
															<asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
														</ItemTemplate>
													</asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Incluir nota/observación</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2">
                                        <asp:RadioButtonList ID="rblPopUp_EventoIncluirNotaOrden" runat="server" RepeatDirection="Horizontal">
						                    <asp:ListItem Text="No Incluir" Value="1" Selected="True"></asp:ListItem>
						                    <asp:ListItem Text="Al Inicio de la sección" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Al Final de la sección" Value="3"></asp:ListItem>
					                    </asp:RadioButtonList>
				                    </td>
			                    </tr>
                                <tr>
				                    <td colspan="4"><asp:TextBox ID="txtPopUp_EventoNotaEventoOrden" runat="server" CssClass="Textarea_General" Height="60px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpnlAcomodo_Evento" runat="server">
                        <HeaderTemplate>Acomodo</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table class="FormTable">
                                <tr>
				                    <td class="Etiqueta">Tipo de Acomodo</td>
				                    <td class="VinetaObligatorio">*</td>
				                    <td class="Campo"><asp:DropDownList ID="ddlPopUp_EventoTipoAcomodo" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td class="Etiqueta">Nombre</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoAcomodoNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="Etiqueta">Puesto</td>
                                    <td class="VinetaObligatorio"></td>
                                    <td class="Campo"><asp:TextBox ID="txtPopUp_EventoAcomodoPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
				                    <td class="Etiqueta"><asp:Button ID="btnAgregarAcomodo" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarAcomodo_Click" /></td>
				                    <td class="Espacio"></td>
				                    <td class="Campo"></td>
                                    <td></td>
			                    </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="border:0px solid #000000; clear:both; position:relative; width:100%;">
                                            <table cellspacing="0" rules="all" border="1" style="border-collapse:collapse; width:620px;">
                                                <tr class="Grid_Header_PopUp">
                                                    <th scope="col" style="width:80px;">Orden</th>
                                                    <th scope="col" style="width:200px;">Nombre</th>
										            <th scope="col">Puesto</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border:1px solid #C1C1C1; height:180px; overflow-x:hidden; overflow-y:scroll; text-align:left; Width:618px">
                                            <asp:GridView ID="gvAcomodo" runat="server" AllowPaging="false" AllowSorting="False" AutoGenerateColumns="False" ShowHeader="false" Width="100%"
                                                DataKeyNames="Orden,Nombre,Puesto"
                                                OnRowCommand="gvAcomodo_RowCommand"
                                                OnRowDataBound="gvAcomodo_RowDataBound">
                                                <RowStyle CssClass="Grid_Row_Scroll" />
                                                <EmptyDataTemplate>
                                                    <div style="border:0px; clear:both; color:#675C9D; font:11px Tahoma; font-weight:normal; height:15px; position:relative; text-align:center; width:100%;">
                                                        No se ha capturado el Acomodo
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="79px"  DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
			                    </tr>
                                <tr>
				                    <td class="Etiqueta">Incluir nota/observación</td>
				                    <td class="Espacio"></td>
				                    <td class="Campo" colspan="2">
                                        <asp:RadioButtonList ID="rblPopUp_EventoIncluirNotaAcomodo" runat="server" RepeatDirection="Horizontal">
						                    <asp:ListItem Text="No Incluir" Value="1" Selected="True"></asp:ListItem>
						                    <asp:ListItem Text="Al Inicio de la sección" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Al Final de la sección" Value="3"></asp:ListItem>
					                    </asp:RadioButtonList>
				                    </td>
			                    </tr>
                                <tr>
				                    <td colspan="4"><asp:TextBox ID="txtPopUp_EventoNotaEventoAcomodo" runat="server" CssClass="Textarea_General" Height="60px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Botones">
                            <asp:Button ID="btnPopUp_EventoCommand" runat="server" Text="" CssClass="Button_General" Width="125px" OnClick="btnPopUp_EventoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes">
                            <asp:Label ID="lblPopUp_EventoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_ActividadGeneral" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ActividadGeneralContent" runat="server" CssClass="PopUpContent" style="margin-top:-130px; margin-left:-260px;" Height="260px" Width="520px">
            <asp:Panel ID="pnlPopUp_ActividadGeneralHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ActividadGeneralTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ActividadGeneral" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ActividadGeneral_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ActividadGeneralBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                        <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                        <td class="Campo">
                            <table style="width:405px">
	                            <tr>
		                            <td colspan="2" style="text-align:left;">
                                        <asp:DropDownList ID="ddlAgrupacion_ActividadGeneral" runat="server" CssClass="DropDownList_General" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupacion_ActividadGeneral_SelectedIndexChanged"></asp:DropDownList>
		                            </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtOtraAgrupacion_ActividadGeneral" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="360px"></asp:TextBox>
		                            </td>
                                    <td style="text-align:right;">
                                        <asp:Button ID="btnNuevaAgrupacion_ActividadGeneral" runat="server" Text="+" CssClass="Button_Special_Gray" Enabled="false" ToolTip="Nueva Agrupacion" Width="25px" OnClick="btnNuevaAgrupacion_ActividadGeneral_Click" />
		                            </td>
	                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUp_ActividadGeneralDetalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
				        <td class="Etiqueta">Desde las</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo">
                            <wuc:wucTimer ID="wucPopUp_ActividadGeneralTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                            <wuc:wucTimer ID="wucPopUp_ActividadGeneralTimerHasta" runat="server" />
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ActividadGeneralCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ActividadGeneralCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ActividadGeneralMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_LugarEvento" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent_LugarEvento" runat="server" CssClass="PopUpContent" style="margin-top:-285px; margin-left:-310px;" Height="570px" Width="620px">
            <asp:Panel ID="pnlPopUpHeader_LugarEvento" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle_LugarEvento" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_LugarEvento" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_LugarEvento_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody_LugarEvento" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estado</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpEstado_LugarEvento" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpEstado_LugarEvento_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Municipio</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpMunicipio_LugarEvento" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpMunicipio_LugarEvento_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Colonia</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
							<asp:TextBox ID="txtPopUpColonia_LugarEvento" runat="server" CssClass="Textbox_General_Disabled" MaxLength="1000" Width="400px" Enabled="false"></asp:TextBox>
							<asp:HiddenField ID="hddPopUpColoniaId_LugarEvento" runat="server" />
							<asp:AutoCompleteExtender
								ID="autosuggestColonia_LugarEvento" 
								runat="server"
								TargetControlID="txtPopUpColonia_LugarEvento"
								ServiceMethod="WSColonia"
                                ServicePath=""
								CompletionInterval="100"
                                DelimiterCharacters=""
                                Enabled="True"
								EnableCaching="False"
								MinimumPrefixLength="2"
								OnClientItemSelected="ColoniaSelected_LugarEvento"
								CompletionListCssClass="Autocomplete_CompletionListElement"
								CompletionListItemCssClass="Autocomplete_ListItem"
								CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                                UseContextKey="true"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Calle</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpCalle_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta"># Ext</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Etiqueta">
                            <asp:TextBox ID="txtPopUpNumeroExterior_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            # Int&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPopUpNumeroInterior_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>
				        </td>
                    </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpDescripcion_LugarEvento" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="140px"></CKEditor:CKEditorControl>
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand_LugarEvento" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUpCommand_LugarEvento_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPopUpMessage_LugarEvento" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="hddGiraConfiguracionId" runat="server" Value="0"  />
    <asp:HiddenField ID="hddSort" runat="server" Value="ConfiguracionHoraInicioEstandar" />
    <asp:HiddenField ID="AgrupacionKey" runat="server" Value=""  />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
