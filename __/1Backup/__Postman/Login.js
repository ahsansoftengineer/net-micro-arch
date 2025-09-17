// Login Post Response
const response = pm?.response?.json();
const record = response?.record;
// Set Environment Variables
if(record?.accessToken){
    const env = pm.collectionVariables;
    env.set("AccessToken", record?.accessToken);
    env.set("AccessTokenExpiry", record?.accessTokenExpiry);
    env.set("RefreshToken", record?.refreshToken);
    env.set("RefreshTokenExpiry", record?.refreshTokenExpiry);
    console.log("AccessToken Saved Successfully");
} else {
    throw new Error("Failed to refresh token. No Result object in response.");
}

// Logout Post Reponse
const now = new Date();
const expiredTime = new Date(now.getTime() - 2 * 60 * 60 * 1000); // 2 hours earlier

const env = pm.collectionVariables;
env.set("AccessTokenExpiry", expiredTime.toISOString());
