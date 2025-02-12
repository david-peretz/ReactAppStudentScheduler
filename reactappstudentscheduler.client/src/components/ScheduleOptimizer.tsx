import React, { useState, useEffect } from 'react';
import { FormControl, InputLabel, Select, MenuItem, Button, Table, TableHead, TableRow, TableCell, TableBody } from '@mui/material';
import api from '../services/api';

interface Course {
  id: number;
  name: string;
}

interface Schedule {
  courseId: number;
  day: string;
  startTime: string;
  endTime: string;
}

const ScheduleOptimizer: React.FC = () => {
  const [courses, setCourses] = useState<Course[]>([]);
  const [selectedCourses, setSelectedCourses] = useState<number[]>([]);
  const [schedule, setSchedule] = useState<Schedule[]>([]);
  const [message, setMessage] = useState<string>("");

  useEffect(() => {
    api.get('/courses').then(response => setCourses(response.data));
  }, []);

  const handleOptimize = async () => {
    try {
      const response = await api.post('/schedules/optimize', {
        studentId: 1,
        courseIds: selectedCourses
      });

      setSchedule(response.data);
      setMessage("Successfully optimized schedule!");
    } catch (error: any) {
      setMessage(error.response?.data || "Failed to optimize schedule.");
    }
  };

  return (
    <div>
      <FormControl fullWidth>
        <InputLabel>Select Courses</InputLabel>
        <Select multiple value={selectedCourses} onChange={e => setSelectedCourses(e.target.value as number[])}>
          {courses.map(course => (
            <MenuItem key={course.id} value={course.id}>{course.name}</MenuItem>
          ))}
        </Select>
        <Button variant="contained" onClick={handleOptimize}>Generate Schedule</Button>
      </FormControl>

      {message && <p>{message}</p>}

      {schedule.length > 0 && (
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Course</TableCell>
              <TableCell>Day</TableCell>
              <TableCell>Time</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {schedule.map((entry, index) => (
              <TableRow key={index}>
                <TableCell>{entry.courseId}</TableCell>
                <TableCell>{entry.day}</TableCell>
                <TableCell>{entry.startTime} - {entry.endTime}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      )}
    </div>
  );
};

export default ScheduleOptimizer;
