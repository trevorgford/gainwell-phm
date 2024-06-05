import React, { useEffect, useState } from 'react';
import { FormGroup, FormControlLabel, Checkbox } from '@mui/material';

interface CheckboxListProps {
    parentId?: number;
    options: CheckboxListProp[];
    initialSelectedOptions?: number[];
    onSelectionChange: (selectedOptions: number[], id?: number) => void;
}

interface CheckboxListProp {
    id: number | undefined;
    description: string | undefined;
}

const CheckboxList: React.FC<CheckboxListProps> = ({ parentId, options, initialSelectedOptions, onSelectionChange }) => {
  const [selectedOptions, setSelectedOptions] = useState<number[]>([]);

  useEffect(() => {
    if(initialSelectedOptions)
    setSelectedOptions(initialSelectedOptions);
  }, [initialSelectedOptions]);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(event.target.value);
    setSelectedOptions(prevSelectedOptions => {
      const newSelectedOptions = prevSelectedOptions.includes(value)
        ? prevSelectedOptions.filter(option => option !== value)
        : [...prevSelectedOptions, value];

      onSelectionChange(newSelectedOptions, parentId);
      return newSelectedOptions;
    });
  };

  return (
    <FormGroup>
        {options.map(option => (
            <FormControlLabel key={option.id}
            control={<Checkbox checked={selectedOptions.includes(option.id!)} onChange={handleChange} value={option.id} />}
            label={option.description} />
        ))}
    </FormGroup>
  );
};

export default CheckboxList;
