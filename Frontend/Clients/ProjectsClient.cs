using Frontend.Models;

namespace Frontend.Clients;

public class ProjectsClient(HttpClient httpClient)
{
    public async Task<ProjectSummary[]> GetProjectsAsync() 
        => await httpClient.GetFromJsonAsync<ProjectSummary[]>("projects") ?? [];

    
    public async Task AddProjectAsync(ProjectSummary project)
        => await httpClient.PostAsJsonAsync("projects", project);

    public async Task<ProjectSummary> GetProjectAsync(int id)
        => await httpClient.GetFromJsonAsync<ProjectSummary>($"projects/{id}") ?? 
            throw new Exception("Project not found");
    
    public async Task UpdateProjectAsync(ProjectSummary updatedProject)
        => await httpClient.PutAsJsonAsync($"projects/{updatedProject.ProjectId}", updatedProject);
    
    public async Task DeleteProjectAsync(int id)
        => await httpClient.DeleteAsync($"projects/{id}");

}
