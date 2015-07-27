<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveDocumentos.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveDocumentos" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:UpdatePanel ID="DocumentUpdate" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
                <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
            </asp:Panel>

            <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Documentos adjuntos al evento"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
                <table class="HeaderTable">
                    <tr>
                        <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Administre los documentos adjuntos al evento."></asp:Label></td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
                <table class="FormTable">
                    <tr>
				        <td class="Etiqueta">Nombre de evento</td>
				        <td class="Espacio"></td>
				        <td class="Campo" colspan="2"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			        </tr>
                    <tr>
				        <td class="Etiqueta">Fecha de evento</td>
				        <td class="Espacio"></td>
				        <td class="Campo" colspan="2"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			        </tr>
                     <tr>
				        <td class="Etiqueta">Tipo de documento</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo"><asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                        <td></td>
			        </tr>
                    <tr>
						<td class="Etiqueta">Adjuntar documento</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo" colspan="2">
                            <asp:FileUpload ID="fupDocumento" runat="server" Width="600px" />
				        </td>
					</tr>
					<tr><td class="Etiqueta" colspan="4" style="text-align:left;">Observaciones</td></tr>
					<tr>
						<td colspan="4"><CKEditor:CKEditorControl ID="ckeDescripcion" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl></td>
					</tr>
                </table>
            </asp:Panel>
	
			<asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
                <%--Empty Content--%>
            </asp:Panel>

            <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="Button_General" width="125px" onclick="btnAgregar_Click" /> &nbsp;&nbsp;
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
                <asp:GridView ID="gvDocumento" runat="server" AllowPaging="false" AllowSorting="true"  AutoGenerateColumns="False" Width="100%"
					DataKeyNames="DocumentoId,ModuloId,UsuarioId,NombreDocumento,Icono"
					OnRowCommand="gvDocumento_RowCommand" 
					OnRowDataBound="gvDocumento_RowDataBound" 
					OnSorting="gvDocumento_Sorting">
					<AlternatingRowStyle CssClass="Grid_Row_Alternating" />
					<HeaderStyle CssClass="Grid_Header" />
					<RowStyle CssClass="Grid_Row" />
					<EmptyDataTemplate>
						<table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
							<tr class="Grid_Header">
								<td style="width:200px;">Nombre</td>
								<td>Descripción</td>
							</tr>
							<tr class="Grid_Row">
								<td colspan="2">No se encontraron Documentos asociados a el evento</td>
							</tr>
						</table>
					</EmptyDataTemplate>
					<Columns>
						<asp:BoundField HeaderText="Nombre"			ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="200px"	DataField="NombreDocumento"						SortExpression="NombreDocumento"></asp:BoundField>
						<asp:BoundField HeaderText="Descripción"	ItemStyle-HorizontalAlign="Left"							DataField="Descripcion"		HtmlEncode="false"	SortExpression="Descripcion"></asp:BoundField>
						<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
							<ItemTemplate>
								<asp:ImageButton ID="imgView" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Visualizar" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
							<ItemTemplate>
								<asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Borrar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
            </asp:Panel>

            <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
                <%--Empty Content--%>
            </asp:Panel>

            <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
            <asp:HiddenField ID="SenderId" runat="server" Value="0" />
            <asp:HiddenField ID="hddSort" runat="server" Value="NombreDocumento" />
            <asp:HiddenField ID="Logistica" runat="server" Value="0" />
	
		</ContentTemplate>
		<Triggers>
			<asp:PostBackTrigger ControlID="btnAgregar" />
		</Triggers>
	</asp:UpdatePanel>

</asp:Content>
