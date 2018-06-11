<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/knockout-2.2.1.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.ui.dialog.js"></script>
    <script type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0"></script>
    <script type="text/javascript" src="../Scripts/wingtip.flightstatus.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Flight Status
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="width:900px;">
        <div id="toolbar" style="width: 200px; margin: 10px; float: left">
            <div style="margin-bottom: 10px;">
                <div>Airport</div>
                <div>
                    <select id="airportCode" data-bind="foreach: get_airports()">
                        <option data-bind="attr: { value: get_code() }, text: '(' + get_code() + ') ' + get_title().substring(0, 10) + ', ' + get_location()" ></option>
                    </select>
                </div>
            </div>
            <div style="margin-bottom: 10px;">
                <div>Carrier</div>
                <div>
                    <select id="carrierCode">
                        <option value="AP">Air One</option>
                        <option value="FL">AirTran</option>
                        <option value="AS">Alaska Airlines</option>
                        <option value="AQ">Aloha Air
                        <option value="HP">America West Airlines</option>
                        <option value="AA">American Airlines</option>
                        <option value="CO">Continental Airlines</option>
                        <option value="DL">Delta Air Lines</option>
                        <option value="HA">Hawaiian Airlines</option>
                        <option value="YX">Midwest Express</option>
                        <option value="NW">Northwest Airlines</option>
                        <option value="WN">Southwest Airlines</option>
                        <option value="FF">Tower Air</option>
                        <option value="UA">United Airlines</option>
                        <option value="US">US Airways</option>
                    </select>
                </div>
            </div>
            <div style="margin-bottom: 10px;">
                <div>Flight Number</div>
                <div>
                    <input id="flightNumber" type="text" required placeholder="686" />
                </div>
            </div>
            <div style="margin-bottom: 10px;">
                <div>Departure Date</div>
                <div>
                    <input id="departureDate" type="text" pattern="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]20\d\d" required placeholder="01/01/2013" />
                </div>
            </div>
            <div style="margin-bottom: 10px;">
                <button id="submitFlight" class="app-button">Get Status</button>
            </div>
        </div>
        <div style="width:600px;float:right;">
            <div id="status" style="margin-bottom: 10px; width:600px;">
                    <table>
                        <caption>Flight Status</caption>
                        <thead>
                            <tr>
                                <th><div style="width:125px">Departing</div></th>
                                <th><div style="width:125px">Time</div></th>
                                <th><div style="width:125px">Arriving</div></th>
                                <th><div style="width:125px">Time</div></th>
                            </tr>
                        </thead>
                        <tbody id="statusTable" data-bind="foreach: get_operationalTimes()">
                            <tr>
                                <td style="text-align:center" data-bind="text: get_departureAirportCode()"></td>
                                <td style="text-align:center" data-bind="text: get_departureDate()"></td>
                                <td style="text-align:center" data-bind="text: get_arrivalAirportCode()"></td>
                                <td style="text-align:center" data-bind="text: get_arrivalDate()"></td>
                            </tr>
                        </tbody>
                    </table>
            </div>
            <div>
                <div id="map" style="position: absolute;width: 600px; height: 400px; margin: 10px; border: 1px solid #ccc"></div>
            </div>
        </div>
    </div>
</asp:Content>
