import React from 'react';
import ReactDOM from 'react-dom/client';
import "./i18n/i18n";

import '@styles/index.scss'; 

import App from './App';
import { GoogleOAuthProvider } from '@react-oauth/google';
import reportWebVitals from './reportWebVitals';
import { LanguageProvider } from "./contexts/LanguageContext";

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <LanguageProvider>
      <GoogleOAuthProvider clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID || ''}>
        <App />
      </GoogleOAuthProvider>
    </LanguageProvider>
  </React.StrictMode>
);

reportWebVitals();