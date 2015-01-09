<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="catLugarEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Catalogo.catLugarEvento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
    <script type = "text/javascript">

        function ColoniaSelected(sender, e) {
            $get("<%=hddPopUpColoniaId.ClientID %>").value = e.get_value();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconDetail.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Catálogo - Lugares de Evento"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Pantalla de administración del catálogo de Lugares de Evento. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtNombre" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Estatus</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Button_General" Width="125px" OnClick="btnBuscar_Click" />&nbsp;
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Button_General" Width="125px" OnClick="btnNuevo_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvLugarEvento" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="LugarEventoId,Activo,Nombre"
            OnRowDataBound="gvLugarEvento_RowDataBound"
            OnRowCommand="gvLugarEvento_RowCommand"
            OnSorting="gvLugarEvento_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 150px;">Nombre</td>
                        <td style="width: 110px;">Municipio</td>
                        <td style="width: 150px;">Colonia</td>
                        <td style="width: 110px;">Calle</td>
                        <td style="width: 75px;"># Ext</td>
                        <td style="width: 75px;"># Int</td>
                        <td style="width: 80px;">Estatus</td>
                        <td>Descripción</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="8">No se encontraron Lugares de Evento registrados en el sistema</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Nombre"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="Nombre"                                  SortExpression="Nombre"></asp:BoundField>
                <asp:BoundField HeaderText="Municipio"      ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="110px" DataField="MunicipioNombre"                         SortExpression="MunicipioNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Colonia"        ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="ColoniaNombre"                           SortExpression="ColoniaNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Calle"          ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="110px" DataField="Calle"                                   SortExpression="Calle"></asp:BoundField>
                <asp:BoundField HeaderText="# Ext"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="75px"  DataField="NumeroExterior"                          SortExpression="NumeroExterior"></asp:BoundField>
                <asp:BoundField HeaderText="# Int"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="75px"  DataField="NumeroInterior"                          SortExpression="NumeroInterior"></asp:BoundField>
                <asp:BoundField HeaderText="Estatus"        ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px"  DataField="Estatus"                                 SortExpression="Estatus"></asp:BoundField>
                <asp:BoundField HeaderText="Descripción"    ItemStyle-HorizontalAlign="Left"                            DataField="Descripcion"         HtmlEncode="false"  SortExpression="Descripcion"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="PopUp" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" style="margin-top:-270px; margin-left:-310px;" Height="540px" Width="620px">
            <asp:Panel ID="pnlPopUpHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Municipio</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpMunicipio" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpMunicipio_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Colonia</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
							<asp:TextBox ID="txtPopUpColonia" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
							<asp:HiddenField ID="hddPopUpColoniaId" runat="server" />
							<asp:AutoCompleteExtender
								ID="autosuggestColonia" 
								runat="server"
								TargetControlID="txtPopUpColonia"
								ServiceMethod="WSColonia"
                                ServicePath=""
								CompletionInterval="100"
                                DelimiterCharacters=""
                                Enabled="True"
								EnableCaching="False"
								MinimumPrefixLength="2"
								OnClientItemSelected="ColoniaSelected"
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
                        <td class="Campo"><asp:TextBox ID="txtPopUpCalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta"># Ext</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Etiqueta">
                            <asp:TextBox ID="txtPopUpNumeroExterior" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            # Int&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPopUpNumeroInterior" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>
				        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpStatus" runat="server" CssClass="DropDownList_General" Width="405px"></asp:DropDownList></td>
                    </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpDescripcion" BasePath="~/Include/Components/CKEditor/Core" runat="server" Height="130px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUpCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPopUpMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <br /><br />

    <asp:HiddenField ID="hddLugarEvento" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
