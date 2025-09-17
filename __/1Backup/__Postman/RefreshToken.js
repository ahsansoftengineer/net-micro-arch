// Collection - Pre-Request
addHeaders();
manageToken();

function manageToken() {
    if (isTokenExpired()) {
        refreshToken();  
    } else {
        console.log("Access token is still valid.");
    }
}
// Function to add headers to the request
function addHeaders() {
    const headers = pm.request.headers;
    headers.add({
        key: "postman-headers",
        value: "Ahsan Postman Headers"
    });
    headers.add({
        key: "device-access-type",
        value: "099f5ef4-69ad-4775-a4a9-f35b74009d05"
    });
}

// Function to check if the token has expired or is close to expiring
function isTokenExpired() {
    const env = pm.collectionVariables;

    const now = new Date();
    const time = now.getTime(); 
    const expire = env.get("AccessTokenExpiry");

    if(expire){
        const accessTokenExpiry = new Date(expire);
        const expiry = accessTokenExpiry.getTime();

        const bufferInMilliseconds = 60 * 1000; 

        const seconds =  (expiry - time) / 1000;
        const minute = seconds / 60;
        const hour = minute / 60;
        console.log("Access Token Expires In => " + (hour < 1 ? 0 : hour.toFixed(1)) + ":" + (minute % 60).toFixed(0) + ":" + (seconds % 60).toFixed(0));

        return (expiry - time) < bufferInMilliseconds;
    } else {
        console.warn("No Collection Variables found")
        return true;
    }
    
}

// Function to refresh the token if expired
function refreshToken() {
    console.log("Access token has expired or is about to expire. Refreshing...");

    // Call the refresh token API
    pm.sendRequest({
        url: 'http://localhost:1104/api/auth/v1/account/login', // Change this to your RefreshToken endpoint
        method: 'POST',
        header: {
            'postman-header': 'Ahsan Header Here', // Corrected headers with key-value pairs
            'device-access-type': '099f5ef4-69ad-4775-a4a9-f35b74009d05',
            'Content-Type': 'application/json', // Added Content-Type header
        },
        body: {
            mode: 'raw',
            raw: JSON.stringify({
                "email": "user@example.com",
                "password": "strings"
            })
        }
    }, function (err, res) {
        if (err) {
            console.error("Token refresh failed:", err);
            throw new Error("Token refresh failed. By Signin in Again.");
        }
        const env = pm.collectionVariables;
        
        const response = res.json();
        const record = response?.record;

        // Assuming the response structure is the same as your original response
        if (record?.accessToken) {
            env.set("AccessToken", record.accessToken);
            env.set("AccessTokenExpiry", record.accessTokenExpiry);
            env.set("RefreshToken", record.refreshToken);
            env.set("RefreshTokenExpiry", record.refreshTokenExpiry);
            console.log("Access token refreshed successfully.");
        } else {
            throw new Error("Failed to refresh token. No Result object in response.");
        }
    });
};