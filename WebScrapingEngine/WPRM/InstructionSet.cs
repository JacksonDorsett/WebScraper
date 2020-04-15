
namespace WebScrapingEngine.WPRM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Set of instructions.
    /// </summary>
    public class InstructionSet
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionSet"/> class.
        /// </summary>
        /// <param name="name">name of section.</param>
        /// <param name="steps">Steps in section.</param>
        public InstructionSet(string name, string[] steps)
        {
            this.SectionName = name;
            this.Steps = steps;
        }

        /// <summary>
        /// Gets section Name.
        /// </summary>
        public string SectionName { get; private set; }

        /// <summary>
        /// Gets Section steps.
        /// </summary>
        public string[] Steps { get; private set; }
    }
}
