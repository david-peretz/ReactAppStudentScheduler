import React, { useState, useEffect } from 'react';
import { FormControl, InputLabel, Select, MenuItem, Button } from '@mui/material';
import api from '../services/api';

interface Course {
  id: number;
  name: string;
}

const CourseSelection: React.FC = () => {
  const [courses, setCourses] = useState<Course[]>([]);
  const [selectedCourses, setSelectedCourses] = useState<number[]>([]);

  useEffect(() => {
    api.get('/courses').then(response => setCourses(response.data));
  }, []);

  const handleSubmit = async () => {
    try {
      const scheduleData = selectedCourses.map(courseId => ({
        studentId: 1, // ודא שהסטודנט קיים במסד הנתונים
        courseId,
        day: "Monday", // יש להתאים לשעות האמיתיות
        startTime: "10:00",
        endTime: "12:00"
      }));

      const response = await api.post('/schedules', scheduleData);
      console.log("Schedule created:", response.data);
      alert("Schedule successfully created!");
    } catch (error) {
      console.error("Error creating schedule:", error);
      alert("Failed to create schedule");
    }
  };

  return (
    <FormControl fullWidth>
      <InputLabel>Select Courses</InputLabel>
      <Select multiple value={selectedCourses} onChange={e => setSelectedCourses(e.target.value as number[])}>
        {courses.map(course => (
          <MenuItem key={course.id} value={course.id}>{course.name}</MenuItem>
        ))}
      </Select>
      <Button variant="contained" onClick={handleSubmit}>Submit</Button>
    </FormControl>
  );
};

export default CourseSelection;
