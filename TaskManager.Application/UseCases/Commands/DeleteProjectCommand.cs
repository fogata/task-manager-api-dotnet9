using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.UseCases.Commands;

public record DeleteProjectCommand(Guid ProjectId);
