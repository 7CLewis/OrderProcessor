namespace OrderProcessor.Producer;
internal sealed class OrdersMCPToolInfo
{
    // Create Order
    public const string CreateOrderToolName = "create_order";
    public const string CreateOrderToolDescription = "Create an order";
    public const string ProductNamesPropertyName = "product_names";
    public const string ProductNamesPropertyDescription = "List of product names to include in the order";
    public const string SenderAddressLine1PropertyName = "sender_address_line_1";
    public const string SenderAddressLine1PropertyDescription = "Sender's address line 1";
    public const string SenderAddressLine2PropertyName = "sender_address_line_2";
    public const string SenderAddressLine2PropertyDescription = "Sender's address line 2";
    public const string SenderAddressCityPropertyName = "sender_address_city";
    public const string SenderAddressCityPropertyDescription = "Sender's address city";
    public const string SenderAddressStatePropertyName = "sender_address_state";
    public const string SenderAddressStatePropertyDescription = "Sender's address state";
    public const string SenderAddressPostalCodePropertyName = "sender_address_postal_code";
    public const string SenderAddressPostalCodePropertyDescription = "Sender's address postal code";
    public const string SenderAddressCountryPropertyName = "sender_address_country";
    public const string SenderAddressCountryPropertyDescription = "Sender's address country";
    public const string ReceiverAddressLine1PropertyName = "receiver_address_line_1";
    public const string ReceiverAddressLine1PropertyDescription = "Receiver's address line 1";
    public const string ReceiverAddressLine2PropertyName = "receiver_address_line_2";
    public const string ReceiverAddressLine2PropertyDescription = "Receiver's address line 2";
    public const string ReceiverAddressCityPropertyName = "receiver_address_city";
    public const string ReceiverAddressCityPropertyDescription = "Receiver's address city";
    public const string ReceiverAddressStatePropertyName = "receiver_address_state";
    public const string ReceiverAddressStatePropertyDescription = "Receiver's address state";
    public const string ReceiverAddressPostalCodePropertyName = "receiver_address_postal_code";
    public const string ReceiverAddressPostalCodePropertyDescription = "Receiver's address postal code";
    public const string ReceiverAddressCountryPropertyName = "receiver_address_country";
    public const string ReceiverAddressCountryPropertyDescription = "Receiver's address country";
    public const string TransportCompanyNamePropertyName = "transport_company_name";
    public const string TransportCompanyNamePropertyDescription = "Name of the transport company to use for the order";
    public const string GetOrderInfoToolName = "get_order_info";
    public const string GetOrderInfoToolDescription = "Get requested information about an order(s)";

    // Get Products
    public const string GetProductsToolName = "get_products";
    public const string GetProductsToolDescription = "Get a list of products";
    public const string ProductNameFilterPropertyName = "product_name_filter";
    public const string ProductNameFilterPropertyDescription = "Substring to search for in product names";

    // Get Transport Companies
    public const string GetTransportCompaniesToolName = "get_transport_companies";
    public const string GetTransportCompaniesToolDescription = "Get a list of transport companies";
    public const string TransportCompanyNameFilterPropertyName = "transport_company_name_filter";
    public const string TransportCompanyNameFilterPropertyDescription = "Substring to search for in transport company names";
}
