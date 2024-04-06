import React from 'react';
import ReactDOM from 'react-dom/client';
import { AppRouter } from './router';
import { ConfigProvider } from 'antd';

import './index.css';
import { theme } from 'theme/theme';
import { Provider } from 'react-redux';
import { store } from 'store';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>

    <ConfigProvider theme={theme}>
      <Provider store={store}>
        <AppRouter />
      </Provider>
    </ConfigProvider>

  </React.StrictMode>
);
