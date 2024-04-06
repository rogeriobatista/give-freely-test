import { Navigate, useLocation } from 'react-router';
import { JSX } from 'react/jsx-runtime';
import { useAppSelector } from 'store/hooks';

export function PrivateRoute({ children }: { children: JSX.Element }) {
  const location = useLocation();
  const { isLoggedIn } = useAppSelector(state => state.login)

  return (
    !isLoggedIn ? (
      <Navigate to="/sign-in" state={{ from: location }} replace />
    ) : (
      children
    )
  )
}
