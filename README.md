Terraform State to Import Command Generator

This tool came about after using the Terrafy tool.  Terrafy generated HCL from an external environment but the generated code needed to be modularised, parameterised and have the resources given meaningful names.

Once complete the productionised HCL was run in a new environment to verify the all was working.  I then took the state file from new environment and use this utility to create a Terraform Input command for each of the resources.  I can then do a find and replace on the subscription ID and then run the commands back in the original environment.  At the end of this process I have the terraform code and state matching the existing environment.

This util is very simple, build it, run it, it takes 2 command line arguments:  the terraform state file (usually terraform.tfstate) and an output file name to write the commands.
