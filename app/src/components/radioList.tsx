import React, { useState, useEffect } from 'react';
import { FormControl, FormControlLabel, RadioGroup, Radio, FormLabel } from '@mui/material';

interface RadioListProps {
    parentId?: number;
    options: RadioListProp[];
    initialSelectedOption?: number | undefined;
    onRadioListSelectionChange: (selectedOption: number, id?: number) => void;
}

interface RadioListProp {
    id: number | undefined;
    description: string | undefined;
}

const RadioList: React.FC<RadioListProps> = ({ parentId, options, initialSelectedOption = undefined, onRadioListSelectionChange }) => {
  const [selectedOption, setSelectedOption] = useState<number | undefined>(initialSelectedOption);

  useEffect(() => {
    setSelectedOption(initialSelectedOption);
  }, [initialSelectedOption]);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(event.target.value);
    setSelectedOption(value);
    onRadioListSelectionChange(value, parentId); // Call the callback with the updated option
  };

  return (
    // <FormControl component="fieldset">
    //   <FormLabel component="legend">{label}</FormLabel>
    //   <RadioGroup value={selectedOption} onChange={handleChange}>
    //     {options.map(option => (
    //       <FormControlLabel
    //         key={option}
    //         value={option}
    //         control={<Radio />}
    //         label={option}
    //       />
    //     ))}
    //   </RadioGroup>
    // </FormControl>
    <RadioGroup value={selectedOption} onChange={handleChange}>
        {options.map(option => (
          <FormControlLabel
            key={option.id}
            value={option.id}
            control={<Radio checked={option.id == selectedOption} />}
            label={option.description}
          />
        ))}
    </RadioGroup>
  );
};

export default RadioList;
