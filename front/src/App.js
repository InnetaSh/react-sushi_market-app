import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MainLayout from './components/layout/MainLayout/MainLayout';
import { routesConfig } from './routes/appRoutes';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          {routesConfig.map((route, index) => (
            route.index ? (
              <Route key={index} index element={route.element} />
            ) : (
              <Route key={index} path={route.path} element={route.element} />
            )
          ))}
        </Route>
      </Routes>
    </Router>
  );
}

export default App;