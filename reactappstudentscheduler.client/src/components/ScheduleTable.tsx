import React, { useEffect, useState } from 'react';
import { Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';
import api from '../services/api';
import { sampleSchedule } from '../services/seedData';

interface ScheduleEntry {
  id: number;
  courseName: string;
  day: string;
  startTime: string;
  endTime: string;
}

const ScheduleTable: React.FC = () => {
  const [schedule, setSchedule] = useState<ScheduleEntry[]>(sampleSchedule);

  useEffect(() => {
    api.get('/schedules')
      .then(response => setSchedule(response.data))
      .catch(error => {
        console.error('Error fetching schedule:', error);
        setSchedule(sampleSchedule); // שימוש בנתוני דמו במקרה של כשל
      });
  }, []);

  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Course</TableCell>
          <TableCell>Day</TableCell>
          <TableCell>Time</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {schedule.length > 0 ? (
          schedule.map(entry => (
            <TableRow key={entry.id}>
              <TableCell>{entry.courseName}</TableCell>
              <TableCell>{entry.day}</TableCell>
              <TableCell>{entry.startTime} - {entry.endTime}</TableCell>
            </TableRow>
          ))
        ) : (
          <TableRow>
            <TableCell colSpan={3} align="center">No schedule available</TableCell>
          </TableRow>
        )}
      </TableBody>
    </Table>
  );
};

export default ScheduleTable;
