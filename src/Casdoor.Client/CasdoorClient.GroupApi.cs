// Copyright 2022 The Casdoor Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Net.Http.Json;

namespace Casdoor.Client;

public partial class CasdoorClient
{
    public virtual async Task<CasdoorResponse?> AddGroupAsync(CasdoorGroup group, CancellationToken cancellationToken = default) =>
        await PostAsJsonAsync(_options.GetActionUrl("add-group"), group, cancellationToken);

    public virtual async Task<CasdoorResponse?> UpdateGroupAsync(CasdoorGroup group, string groupId,
        CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("update-group", new QueryMapBuilder().Add("id", groupId).QueryMap);
        return await PostAsJsonAsync(url, group, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> DeleteGroupAsync(CasdoorGroup group, CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("delete-group");
        return await PostAsJsonAsync(url, group, cancellationToken);
    }

    public virtual async Task<CasdoorGroup?> GetGroupAsync(string name, string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", $"{owner ?? _options.OrganizationName}/{name}").QueryMap;
        string url = _options.GetActionUrl("get-group", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<CasdoorGroup?>();
    }

    public virtual async Task<IEnumerable<CasdoorGroup>?> GetGroupsAsync(string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner ?? _options.OrganizationName).QueryMap;
        string url = _options.GetActionUrl("get-groups", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorGroup>?>();
    }
}
