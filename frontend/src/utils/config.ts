export const { 
  VITE_OIDC_AUTHORITY, 
  VITE_OIDC_CLIENT_ID, 
  VITE_CLIENT_URL, 
  VITE_APP_TITLE, 
  VITE_OIDC_CLIENT_SECRET,
  VITE_OIDC_REDIRECT_URI
} =
  import.meta.env;

export const oidcConfig = {
  authority: VITE_OIDC_AUTHORITY,
  client_id: VITE_OIDC_CLIENT_ID,
  client_secret: VITE_OIDC_CLIENT_SECRET,
  redirect_uri: `${VITE_OIDC_REDIRECT_URI}`,
  response_type:"code",
};