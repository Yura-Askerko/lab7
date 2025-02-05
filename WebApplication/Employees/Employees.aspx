﻿<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="WebApplication.Employees.Employees" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>

        <asp:GridView runat="server" ID="EmployeesGridView"
            AutoGenerateColumns="false"
            AllowPaging="true" 
            AutoGenerateDeleteButton="true"
            AutoGenerateEditButton="true" 
            OnRowCancelingEdit="EmployeesGridView_RowCancelingEdit"
            OnRowDeleting="EmployeesGridView_RowDeleting"
            OnRowEditing="EmployeesGridView_RowEditing"
            OnRowUpdating="EmployeesGridView_RowUpdating"
            PageSize="10" 
            OnPageIndexChanging="EmployeesGridView_PageIndexChanging"
            Caption="Employees" 
            EmptyDataText="Empty" 
            CaptionAlign="Top" 
            PageIndex="0">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="true" />    
                <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
            </Columns>
        </asp:GridView>

    </div>
    
    <div style="margin-top: 10px;">
        <asp:Label ID="AddStatusLabel" runat="server" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
        <h2>Add employee</h2>

        <div>
            <span>Full Name: </span>
            <asp:TextBox ID="EmployeeFullNameTextBox" runat="server"></asp:TextBox>
            <span>Position: </span>
            <asp:TextBox ID="EmployeePositionTextBox" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Button ID="AddEmployeeButton" runat="server" Text="Add service type" OnClick="AddEmployeeButton_Click" />
        </div>
    </div>
    
</asp:Content>
