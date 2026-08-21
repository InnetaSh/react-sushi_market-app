
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import MainLayout from './components/layout/MainLayout/MainLayout';
import MainPage from './pages/MainPage';
import MenuPage from './pages/MenuPage';
import PromotionDetailsPage from './pages/PromotionDetailsPage';
import NewsPage from './pages/NewsPage';
import ContactsPage from '@pages/ContactsPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<MainPage />} />
          <Route path="menu" element={<MenuPage />} />
          <Route path="menu/search/category/:category" element={<MenuPage />} />
          <Route path="sale" element={<PromotionDetailsPage />} />
          <Route path="news" element={<NewsPage />} />
          <Route path="contacts" element={<ContactsPage />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
