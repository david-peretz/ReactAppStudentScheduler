import React from 'react';
import { Container, Typography } from '@mui/material';
import CourseSelection from './components/CourseSelection';
import ScheduleTable from './components/ScheduleTable';

const App: React.FC = () => {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Student Course Scheduler
      </Typography>
      <CourseSelection />
      <ScheduleTable />
    </Container>
  );
};

export default App;