// See https://aka.ms/new-console-template for more information

using LibGit2Sharp;

Repository.Clone("https://github.com/VismaLietuva/simoona.git", "./repo");

// Open the repository
using (var repo = new Repository("./repo"))
{
    // Get the commit history
    var commits = repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = repo.Head });

    // Get a list of all files in the repository
    var files = new List<string>();
    foreach (var commit in commits)
    {
        if (commit.Parents.Count() > 0)
        {
            var oldTree = commit.Parents.First().Tree;
            var newTree = commit.Tree;
            var changes = repo.Diff.Compare<TreeChanges>(oldTree, newTree);
            files.AddRange(changes.Select(c => c.Path));
        }
        else
        {
            files.AddRange(commit.Tree.Select(t => t.Path));
        }
    }

    // Count the frequency of each file
    var fileCounts = files.GroupBy(f => f).ToDictionary(g => g.Key, g => g.Count());

    // Find the most frequently changed file
    var mostChangedFile = fileCounts.OrderByDescending(kvp => kvp.Value).First().Key;

    Console.WriteLine("The most frequently changed file is: {0}", mostChangedFile);
}