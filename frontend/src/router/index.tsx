import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { SignInPage, SignOutPage, HomePage, EmployeesPage } from 'pages';
import { PrivateRoute } from 'utils/privateRoutes';
import { NotFound, AppLayout } from 'components';

export const AppRouter = () => {
  return (
    <Router>
      <Routes>
        <Route
          path='/'
          Component={AppLayout}
        >
          <Route
            path=''
            element={
              <PrivateRoute>
                <HomePage />
              </PrivateRoute>
            } />
          <Route
            path='/employees'
            element={
              <PrivateRoute>
                <EmployeesPage />
              </PrivateRoute>
            } />
        </Route>

        {/* No auth area */}
        <Route path='/sign-in' element={<SignInPage />} />
        <Route path='/sign-out' element={<SignOutPage />} />
        <Route path='*' element={<NotFound />} />
      </Routes>
    </Router>
  );
};
