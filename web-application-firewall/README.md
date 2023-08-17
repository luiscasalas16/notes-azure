# example-azure / web-application-firewall

[Azure Web Application Firewall Documentation](https://learn.microsoft.com/en-us/azure/web-application-firewall)

- [Comandos](#comandos)

- [Web Application Firewall on Azure Application Gateway](https://learn.microsoft.com/en-us/azure/web-application-firewall/ag/ag-overview)
- [](https://medium.com/globant/configure-web-application-firewall-waf-with-azure-application-gateway-68961542f4e2)
- [](https://azure.github.io/Azure-Proactive-Resiliency-Library/services/networking/web-application-firewall)

---

## Comandos

Comandos generales para la administración de un Web Application Firewall.

```powershell
#

```

Geo-filtering
By default, WAF responds to all user requests regardless of location where the request is coming from. In some scenarios, you may want to restrict the access to your web application by countries/regions. The geo-filtering custom rule enables you to define a specific path on your endpoint to either allow or block access from specified countries/regions. The geo-filtering rule uses a two-letter country/region code of interest.
For a geo-filtering rule, a match variable is either RemoteAddr or SocketAddr. RemoteAddr is the original client IP address that is usually sent via X-Forwarded-For request header. SocketAddr is the source IP address that WAF sees. If your user is behind a proxy, SocketAddr is often the proxy server address.
You can combine a GeoMatch condition and a REQUEST_URI string match condition to create a path-based geo-filtering rule.

IP restriction
Azure Web Application Firewall custom rules control access to web applications by specifying a list of IP addresses or IP address ranges.
The IP restriction custom rule lets you control access to your web applications. It does this by specifying an IP address or an IP address range in Classless Inter-Domain Routing(CIDR) format.
By default, your web application is accessible from the Internet. However sometimes, you want to limit access to clients from a list of known IP address or IP address ranges. You can achieve this by creating an IP matching rule that blocks access to your web app from IP addresses s not listed in the custom rule.

Rate limiting
Azure Web Application Firewall custom rules support rate limiting to control access based on matching conditions and the rates of incoming requests.
This custom rule enables you to detect abnormally high levels of traffic and block some types of application layer denial of service attacks. Rate limiting also protects you against clients that have accidentally been misconfigured to send large volumes of requests in a short time period. The custom rule is defined by the rate limit counting duration (either one minute or five-minute intervals) and the rate limit threshold (the maximum number of requests allowed in the rate limit duration).
